using MobileBanking.Data;
using MobileBanking.Data.Models;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Add(CreateProductVM product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                IsActive = true,
                Description = product.Description,
                ProductId = product.ProductId,
                ServiceProviderId = product.ServiceProviderId
            };
            _productRepository.Add(newProduct);
            return newProduct;
        }
    }
}
