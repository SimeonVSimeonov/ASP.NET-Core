using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models.BindingModel
{
    public class EventCreateBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Total Tickets must be a positive number!")]
        [Display(Name = "TotalTicket")]
        public int TotalTicket { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage =
            "Price Per Ticket must be a positive number!")]
        [Display(Name = "PricePerTicket")]
        public decimal PricePerTicket { get; set; }

    }
}
