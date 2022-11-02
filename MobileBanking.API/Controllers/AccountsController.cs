using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileBanking.API.Attributes;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Exceptions;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBanking.API.Controllers
{
    [ApiKey]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILoggingService _loggingService;

        public AccountsController(IAccountService accountService, ILoggingService loggingService)
        {
            _accountService = accountService;
            _loggingService = loggingService;
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult GetAccountDetails(string accountNumber)
        {
            try
            {
                var res = _accountService.GetAccountDetails(accountNumber);
                return Ok(res);
            }
            catch(InvalidAccountExceptions exp)
            {
                return new NotFoundObjectResult(exp.Message);
            }
            catch(Exception exp)
            {
                return new BadRequestObjectResult("Error retrieving account information");
            }
        }
    }
}
