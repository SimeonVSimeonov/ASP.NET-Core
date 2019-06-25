using System.ComponentModel.DataAnnotations;

namespace Panda.Models.BindingModels
{
    public class CreatePackageInputModel
    {
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Weight { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 10)]
        public string ShippingAddress { get; set; }

        [Required]
        public string Recipient { get; set; }
    }
}
