using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Requests
{
    public class CreateCustomerAccountVM
    {
        public int UserId { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
