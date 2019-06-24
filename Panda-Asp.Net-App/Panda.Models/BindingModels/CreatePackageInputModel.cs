using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models.BindingModels
{
    public class CreatePackageInputModel
    {
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }
    }
}
