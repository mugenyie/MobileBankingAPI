using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBanking.MTN.Services
{
    public class MTNService : IMTNService
    {
        private readonly string BaseURL = "https://uganda.api.mtn.com";
        private const string API_KEY = "xxxxx";
        RestClient _restClient;

        public MTNService()
        {
            _restClient = new RestClient(BaseURL);
        }

        public async Task<object> TransferToMobileasync(decimal amount, string recipient)
        {
            var request = new RestRequest("v1/transfer", Method.Post);
            request.AddHeader("apiKey", API_KEY);
            RestResponse response = await _restClient.ExecuteAsync<object>(request);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<object> ValidateMSISDNAsync(string MSISDN)
        {
            var request = new RestRequest("v1/validate", Method.Post);
            request.AddHeader("apiKey", API_KEY);
            RestResponse response = await _restClient.ExecuteAsync<object>(request);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
