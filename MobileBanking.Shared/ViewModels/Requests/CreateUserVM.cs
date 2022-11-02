using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Requests
{
    public class CreateUserVM
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
