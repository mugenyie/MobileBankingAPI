using MobileBanking.Data.Models;
using MobileBanking.Shared.Enums;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Services.Interfaces
{
    public interface ITransactionLogService
    {
        List<TransactionLog> GetHistory(string accountNumber);
        TransactionLog Add(InitiateTransactionRequest transactionRequest, TransactionType transactionType);
    }
}
