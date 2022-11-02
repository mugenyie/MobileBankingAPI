using MobileBanking.Data;
using MobileBanking.Data.Models;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.Enums;
using MobileBanking.Shared.Exceptions;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileBanking.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly IRepository<TransactionLog> _transactionLogRepository;
        private readonly IRepository<Account> _accountRepository;

        public TransactionLogService(IRepository<TransactionLog> transactionLogRepository, IRepository<Account> accountRepository)
        {
            _transactionLogRepository = transactionLogRepository;
            _accountRepository = accountRepository;
        }


        public TransactionLog Add(InitiateTransactionRequest request, TransactionType transactionType)
        {
            decimal transactionAmount;
            if (transactionType == TransactionType.DEBIT)
                transactionAmount = -request.Amount;
            else
                transactionAmount = request.Amount;

            var account = _accountRepository.Query()
                .Where(x => x.AccountNumber == request.AccountNumber).FirstOrDefault();

            if (account == null)
                throw new InvalidAccountExceptions("Account does not exist");

            decimal oldBalance = account.NewBalance;
            decimal newBalance = account.NewBalance + transactionAmount;

            account.NewBalance = newBalance;
            _accountRepository.Update(account);

            var transaction = new TransactionLog
            {
                AccountNumber = request.AccountNumber,
                Amount = transactionAmount,
                Description = request.Description,
                NewBalance = newBalance,
                PaymentStatus = PaymentStatus.SUCCESSFUL,
                PaymentReference = "account debit",
                OrderStatus = OrderStatus.PENDING,
                RecipientId = request.RecipientPhoneNumber,
                RecipientName = request.RecipientName,
                TransactionStatus = TransactionStatus.PENDING
            };
            _transactionLogRepository.Add(transaction);
            return transaction;
        }

        public List<TransactionLog> GetHistory(string accountNumber)
        {
            DateTime minDate = DateTime.UtcNow.AddMonths(-6);

            var transactions = _transactionLogRepository.Query()
                .Where(x => x.CreatedOnUTC >= minDate && x.AccountNumber.Equals(accountNumber))
                .ToList();

            return transactions;
        }
    }
}
