using MobileBanking.Data.Models;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Services.Interfaces
{
    public interface IAccountService
    {
        CustomerAccount CreateCustomerAccount(CreateCustomerAccountVM user);
        Account GetAccountDetails(string accountNumber);
    }
}
