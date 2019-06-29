using Eventures.Data;
using Eventures.Data.Entities;
using Eventures.Models.BindingModel;
using Eventures.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext context;
        public EventsController(EventuresDbContext context)
        {
            this.context = context;
        }

        public IActionResult All()
        {
            List<EventAllViewModel> events = context.Events
                .Select(eventsFromDb => new EventAllViewModel
                {
                    Name = eventsFromDb.Name,
                    Place = eventsFromDb.Place,
                    Start = eventsFromDb.Start
                    .ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    End = eventsFromDb.End
                    .ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
                }).ToList();

            return View(events);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(EventCreateBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                Event eventFromDb = new Event
                {
                    Name = bindingModel.Name,
                    Place = bindingModel.Place,
                    Start = bindingModel.Start,
                    End = bindingModel.End,
                    TotalTickets = bindingModel.TotalTicket,
                    PricePerTicket = bindingModel.PricePerTicket
                };

                context.Events.Add(eventFromDb);
                context.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View();
        }
    }
}