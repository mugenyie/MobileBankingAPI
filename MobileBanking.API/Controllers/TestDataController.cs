using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Helpers;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IServiceProviderService _serviceProviderService;
        private readonly IProductService _productService;

        public TestDataController(
            ICustomerService customerService, 
            IAccountService accountService,
            IServiceProviderService serviceProviderService,
            IProductService productService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _serviceProviderService = serviceProviderService;
            _productService = productService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateCustomer([FromBody] CreateUserVM user)
        {
            try
            { 
                var res = _customerService.Create(user);
                return Ok(res);
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult("Error creating user, contact Admin");
            }
        }

        [HttpPost]
        [Route("CreateCustomerAccount")]
        public IActionResult AddCustomerAccount([FromBody] CreateCustomerAccountVM user)
        {
            try
            {
                var res = _accountService.CreateCustomerAccount(user);
                return Ok("Success");
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult("Error creating user, contact Admin");
            }
        }

        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult AddProduct([FromBody] CreateProductVM  product)
        {
            try
            {
                var res = _productService.Add(product);
                return Ok("Success");
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult("Error creating product, contact Admin");
            }
        }

        [HttpPost]
        [Route("CreateServiceProvider")]
        public IActionResult AddServiceProvider([FromBody] CreateServiceProviderVM service)
        {
            try
            {
                var res = _serviceProviderService.Add(service);
                return Ok("Success");
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult("Error creating service, contact Admin");
            }
        }

        [HttpGet]
        [Route("GenerateBlackList")]
        public async Task<IActionResult> GenerateBlackListAsync(int count = 10000)
        {
            await GeneratePhoneNumbersToFileAsync("BlackList.txt", count);
            return Ok("Success");
        }

        [HttpGet]
        [Route("GenerateWhiteList")]
        public async Task<IActionResult> GenerateWhiteListAsync(int count = 10000)
        {
            await GeneratePhoneNumbersToFileAsync("WhiteList.txt", count);
            return Ok("Success");
        }

        #region helper methods
        private async Task GeneratePhoneNumbersToFileAsync(string filename, int count)
        {
            string filePath = Path.Combine("StaticFiles", filename);

            using (var fileStream = System.IO.File.Create(filePath))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    for (int i = 0; i < count; i++)
                    {
                        string number = $"07{StringHelpers.GenerateRandomNumber(10000000, 99999999)}";
                        await writer.WriteLineAsync(number);
                    }
                }
            }
        }
        #endregion helper methods
    }
}
