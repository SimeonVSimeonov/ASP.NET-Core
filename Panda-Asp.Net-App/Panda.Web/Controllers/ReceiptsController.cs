using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Domain;
using Panda.Models.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Panda.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext context;

        public ReceiptsController(PandaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult My()
        {
            List<ReceiptsMyViewModel> receiptsMyViews = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Where(receipt => receipt.Recipient.UserName == this.User.Identity.Name)
                .Select(receipt => new ReceiptsMyViewModel
                {
                    Id = receipt.Id,
                    Fee = receipt.Fee,
                    IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Recipient = receipt.Recipient.UserName
                })
                .ToList();

            return this.View(receiptsMyViews);
        }

        [HttpGet("/Receipts/Details/{id}")]
        [Authorize]
        public IActionResult Details(string id)
        {
            Receipt receiptFromDb = this.context.Receipts
                .Where(receipt => receipt.Id == id)
                .Include(receipt => receipt.Package)
                .Include(receipt => receipt.Recipient)
                .SingleOrDefault();

            ReceiptDetailsViewModel receiptDetails = new ReceiptDetailsViewModel
            {
                Id = receiptFromDb.Id,
                Fee = receiptFromDb.Fee,
                Recipient = receiptFromDb.Recipient.UserName,
                DeliveryAddress = receiptFromDb.Package.ShippingAddress,
                Description = receiptFromDb.Package.Description,
                Weight = receiptFromDb.Package.Weight,
                IssuedOn = receiptFromDb.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return this.View(receiptDetails);
        }
    }
}