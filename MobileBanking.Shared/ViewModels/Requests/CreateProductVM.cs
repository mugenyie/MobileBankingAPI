using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Requests
{
    public class CreateProductVM
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceProviderId { get; set; }
    }
}
