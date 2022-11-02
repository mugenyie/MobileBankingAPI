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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customersRepository;

        public CustomerService(IRepository<Customer> customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public Customer Authenticate(UserLoginVM user)
        {
            var customer = _customersRepository.Query()
                .Where(u => u.EmailAddress.Equals(user.Email.ToLower()))
                .FirstOrDefault();

            if (customer == null)
                throw new InvalidAccountExceptions("user not found");

            var hashedPassword = PasswordHelpers.ComputeHash(Encoding.UTF8.GetBytes(user.Password), Encoding.UTF8.GetBytes(customer.PasswordSalt));

            if (customer.PasswordHash != hashedPassword)
                throw new InvalidAccountExceptions("wrong credentials");

            return customer;
        }

        public Customer Create(CreateUserVM user)
        {
            string passwordSalt = PasswordHelpers.GenerateSalt();
            string passwordHash = PasswordHelpers.ComputeHash(Encoding.UTF8.GetBytes(user.Password), Encoding.UTF8.GetBytes(passwordSalt));

            Customer customer = new Customer
            {
                FullName = user.FullName,
                EmailAddress = user.Email.ToLower(),
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                IsActive = true
            };
            _customersRepository.Add(customer);

            return customer;
        }
    }
}
