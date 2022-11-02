using MobileBanking.Data;
using MobileBanking.Data.Models;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly IRepository<ServiceProvider> _serviceProviderRepository;

        public ServiceProviderService(IRepository<ServiceProvider> serviceProviderRepository)
        {
            _serviceProviderRepository = serviceProviderRepository;
        }

        public ServiceProvider Add(CreateServiceProviderVM service)
        {
            var newService = new ServiceProvider
            {
                Name = service.Name,
                Description = service.Description,
                IsActive = true
            };
            _serviceProviderRepository.Add(newService);
            return newService;
        }
    }
}
