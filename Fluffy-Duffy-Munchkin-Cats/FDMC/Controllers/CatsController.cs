using FDMC.Models;
using FDMC.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FDMC.Controllers
{
    public class CatsController : Controller
    {
        private readonly FDMCDbContext context;

        public CatsController(FDMCDbContext context)
        {
            this.context = context;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCatInputViewModel model)
        {

            var cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };

            this.context.Cats.Add(cat);
            this.context.SaveChanges();

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var catFromDb = this.context.Cats.SingleOrDefault(cat => cat.Id == id);

            var catView = new DetailsCatViewModel()
            {
                Name = catFromDb.Name,
                Age = catFromDb.Age,
                Breed = catFromDb.Breed,
                ImageUrl = catFromDb.ImageUrl
            };

            return this.View(catView);
        }
    }
}