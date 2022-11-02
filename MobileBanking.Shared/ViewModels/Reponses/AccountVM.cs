using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Reponses
{
    public class AccountVM
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewBalance { get; set; }
    }
}
