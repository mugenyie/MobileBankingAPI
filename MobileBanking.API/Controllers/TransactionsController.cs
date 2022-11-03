using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileBanking.API.Attributes;
using MobileBanking.API.Helpers;
using MobileBanking.ServiceProviders.Interfaces;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Enums;
using MobileBanking.Shared.Exceptions;
using MobileBanking.Shared.Helpers;
using MobileBanking.Shared.ViewModels.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBanking.API.Controllers
{
    [ApiKey]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionLogService _transactionService;
        private readonly ILoggingService _loggingService;
        private readonly ICachingService _cachingService;

        public TransactionsController(ITransactionLogService transactionService, ILoggingService loggingService, ICachingService cachingService)
        {
            _transactionService = transactionService;
            _loggingService = loggingService;
            _cachingService = cachingService;
        }

        [HttpPost]
        [Route("Initiate")]
        public async Task<IActionResult> InitiateTransactionAsync([FromBody] InitiateTransactionRequest transactionRequest)
        {
            Request.Headers.TryGetValue("user-id", out var headerUserId);
            int.TryParse(headerUserId, out int userIdHeader);
            if (userIdHeader != transactionRequest.UserId)
                return new BadRequestObjectResult("Invalid user");

            if (!StringHelpers.IsValidPhoneNumber(transactionRequest.RecipientPhoneNumber))
            {
                ModelState.AddModelError(nameof(transactionRequest.RecipientPhoneNumber), "Invalid phone number format");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (PhoneListingHelper.CheckIsWhitelisted(transactionRequest.RecipientPhoneNumber))
            {
                string cacheKey = $"{transactionRequest.UserId}_{Guid.NewGuid():N}";
                await _cachingService.Set(cacheKey, transactionRequest, 600);
                return Ok(new { TransactionToken = cacheKey, TransactionRequest = transactionRequest });
            }
            else
            {
                if (PhoneListingHelper.CheckIsBlacklisted(transactionRequest.RecipientPhoneNumber))
                {
                    return new BadRequestObjectResult("Phone Number Blacklisted");
                }
            }
            return new BadRequestObjectResult("Unsupported Phone Number");
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmTransactionAsync([FromBody] ConfirmTransactionVM confirmTransaction)
        {
            try
            {
                var transactionRequest = await _cachingService.Get<InitiateTransactionRequest>(confirmTransaction.TransactionToken);

                if (transactionRequest != null)
                {
                    var transaction = _transactionService.Add(transactionRequest, TransactionType.DEBIT);
                    if (transaction != null)
                    {
                        _transactionService.ProcessOrder(transaction);
                    }
                    return Ok(new { Message = "Transaction Processing" });
                }
                return new BadRequestObjectResult("Transaction Expired after 5minutes");
            }catch(InvalidAccountExceptions exp)
            {
                return new BadRequestObjectResult(exp.Message);
            }catch(Exception exp)
            {
                return new BadRequestObjectResult("Error performing transaction, contact admin");
            }
        }

        [HttpGet]
        [Route("History/{accountNumber}")]
        public IActionResult GetTransactionHistory(string accountNumber, DateTime fromDate, DateTime to)
        {
            var transactions = _transactionService.GetHistory(accountNumber);
            return Ok(transactions);
        }
    }
}
