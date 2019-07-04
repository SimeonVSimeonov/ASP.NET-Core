using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Data.Entities
{
    public class Event
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalTickets { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal PricePerTicket { get; set; }
    }
}
