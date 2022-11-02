using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileBanking.API.Attributes;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Exceptions;
using MobileBanking.Shared.Helpers;
using MobileBanking.Shared.ViewModels;
using MobileBanking.Shared.ViewModels.Reponses;
using MobileBanking.Shared.ViewModels.Requests;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBanking.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILoggingService _loggingService;

        public CustomersController(ICustomerService customerService, ILoggingService loggingService)
        {
            _customerService = customerService;
            _loggingService = loggingService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]
        public IActionResult Authenticate([FromBody] UserLoginVM user)
        {
            try
            {
                string token = "";
                var customer = _customerService.Authenticate(user);

                if (customer != null)
                {
                    token = JWTTokenHelpers.GenerateToken(customer.EmailAddress);
                }

                var customerVm = Mapper.Map<CustomerLoginResponse>(customer);
                List<AccountVM> accounts = new List<AccountVM>();
                foreach (var account in customer.CustomerAccounts)
                {
                    accounts.Add(new AccountVM
                    {
                        Name = account.Account.Name,
                        AccountNumber = account.Account.AccountNumber,
                        Description = account.Account.Description,
                        NewBalance = account.Account.NewBalance
                    });
                }
                customerVm.Accounts = accounts;

                return Ok(new { User = customerVm, Token = token });
            }
            catch (InvalidAccountExceptions exp)
            {
                return new BadRequestObjectResult(exp.Message);
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult("Error logging in, contact Admin");
            }
        }
    }
}
