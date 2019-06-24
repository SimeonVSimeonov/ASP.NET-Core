using Panda.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models.ViewModels
{
    public class ReceiptsViewModel
    { 
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }

        public string Details { get; set; }
    }
}
