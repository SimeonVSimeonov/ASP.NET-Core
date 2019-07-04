using Eventures.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models.BindingModel
{
    public class EventCreateBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        [DateGreaterThanAttribute("End")]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
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
