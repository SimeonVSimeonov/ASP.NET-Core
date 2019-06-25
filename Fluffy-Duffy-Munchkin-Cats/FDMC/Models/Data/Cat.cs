using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Models.Data
{
    public class Cat
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 50)]
        public int Age { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Breed { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
