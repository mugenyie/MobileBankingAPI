using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBanking.ServiceProviders.Interfaces
{
    public interface ICachingService
    {
        Task Set(string key, object value, int timeInSeconds = 60);
        Task<T> Get<T>(string key);
    }
}
