using MobileBanking.Data.Models;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer Authenticate(UserLoginVM user);
        Customer Create(CreateUserVM user);
    }
}
