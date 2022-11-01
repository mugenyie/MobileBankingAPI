using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBanking.MTN.Services
{
    public interface IMTNService
    {
        Task<object> ValidateMSISDNAsync(string MSISDN);
        Task<object> TransferToMobileasync(decimal amount, string recipient);
    }
}
