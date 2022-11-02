using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Reponses
{
    public class CustomerLoginResponse
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public List<AccountVM> Accounts { get; set; }
    }
}
