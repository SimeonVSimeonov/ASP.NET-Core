using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models.ViewModels
{
    public class PackageDeliveredViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }

        public double Weight { get; set; }
    }
}
