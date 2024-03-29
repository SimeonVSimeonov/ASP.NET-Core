﻿namespace Panda.Models.ViewModels
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public string IssuedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public double Weight { get; set; }

        public string Description { get; set; }

        public string Recipient { get; set; }

        public decimal Fee { get; set; }
    }
}
