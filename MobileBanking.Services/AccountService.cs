using MobileBanking.Data;
using MobileBanking.Data.Models;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Exceptions;
using MobileBanking.Shared.Helpers;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileBanking.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerAccount> _customerAccountRepository;

        public AccountService(IRepository<Customer> customerRepository, IRepository<Account> accountRepository, IRepository<CustomerAccount> customerAccountRepository)
        {
            _accountRepository = accountRepository;
            _customerAccountRepository = customerAccountRepository;
            _customerRepository = customerRepository;
        }

        public CustomerAccount CreateCustomerAccount(CreateCustomerAccountVM acc)
        {
            var customer = _customerRepository.GetById(acc.UserId);
            if(customer != null)
            {
                var account = new Account
                {
                    AccountNumber = StringHelpers.GenerateRandomNumber(),
                    Name = acc.Name,
                    Description = acc.Description,
                    OpeningBalance = acc.OpeningBalance,
                    NewBalance = acc.OpeningBalance,
                    IsActive = true
                };
                _accountRepository.Add(account);

                var customerAccount = new CustomerAccount
                {
                    AccountId = account.AccountId,
                    CustomerId = customer.CustomerId
                };
                _customerAccountRepository.Add(customerAccount);

                return customerAccount;
            }
            else
            {
                throw new Exception("Customer does not exist");
            }
        }

        public Account GetAccountDetails(string accountNumber)
        {
            var result = _accountRepository.Query()
                .Where(x => x.AccountNumber.Equals(accountNumber))
                .FirstOrDefault();

            if (result != null)
                return result;
            throw new InvalidAccountExceptions("Account not found");
        }
    }
}
