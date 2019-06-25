using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Domain;
using Panda.Models.BindingModels;
using Panda.Models.ViewModels;

namespace Panda.Web.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PandaDbContext context;

        public PackagesController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreatePackageInputModel inputModel)
        {
            Package package = new Package
            {
                Description = inputModel.Description,
                Recipient = this.context.Users.SingleOrDefault(user => user.UserName == inputModel.Recipient),
                ShippingAddress = inputModel.ShippingAddress,
                Weight = inputModel.Weight,
                Status = this.context.StatusPackage.SingleOrDefault(status => status.Name == "Pending")
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Pending");
        }

        [HttpGet("/Packages/Ship/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.StatusPackage.SingleOrDefault(status => status.Name == "Shipped");
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(new Random().Next(20, 40));
            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Shipped");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Pending")
                .ToList()
                .Select(package => new PackagePendingViewModel
                {
                    Description = package.Description,
                    Weight = package.Weight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.UserName,
                    Id = package.Id
                }).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Shipped")
                .ToList()
                .Select(package => new PackageShippedViewModel
                {
                    Description = package.Description,
                    Weight = package.Weight,
                    EstimatedDeliveryDate = package.EstimatedDeliveryDate?.ToString("dd/MM/yyyy"
                    ,CultureInfo.InvariantCulture),
                    Recipient = package.Recipient.UserName,
                    Id = package.Id
                }).ToList());
        }

        [HttpGet("/Packages/Deliver/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.StatusPackage.SingleOrDefault(status => status.Name == "Delivered");
            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Delivered");
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Delivered" || package.Status.Name == "Acquire")
                .ToList()
                .Select(package => new PackageDeliveredViewModel
                {
                    Description = package.Description,
                    Weight = package.Weight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.UserName,
                    Id = package.Id
                }).ToList());
        }

        [HttpGet("/Packages/Details/{id}")]
        [Authorize]
        public IActionResult Details(string id)
        {
            Package package = this.context.Packages
                .Where(packageFromDb => packageFromDb.Id == id)
                .Include(packageFromDb => packageFromDb.Recipient)
                .Include(packageFromDb => packageFromDb.Status)
                .SingleOrDefault();

            PackageDetailsViewModel viewModel = new PackageDetailsViewModel()
            {
                Description = package.Description,
                Recipient = package.Recipient.UserName,
                ShippingAddress = package.ShippingAddress,
                Status = package.Status.Name,
                Weight = package.Weight
            };

            if (package.Status.Name == "Pending")
            {
                viewModel.EstimatedDeliveryDate = "N/A";
            }
            else if (package.Status.Name == "Shipped")
            {
                viewModel.EstimatedDeliveryDate = package.EstimatedDeliveryDate?
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                viewModel.EstimatedDeliveryDate = "Delivered";
            }

            return this.View(viewModel);
        }

        [HttpGet("/Packages/Acquire/{id}")]
        [Authorize]
        public IActionResult Acquire(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.StatusPackage.SingleOrDefault(status => status.Name == "Acquired");
            this.context.Update(package);

            Receipt receipt = new Receipt
            {
                Fee = (decimal)(2.67 * package.Weight),
                IssuedOn = DateTime.UtcNow,
                Package = package,
                Recipient = context.Users.SingleOrDefault(user => user.UserName == this.User.Identity.Name)
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();

            return this.Redirect("/Home/Index");
        }
    }
}