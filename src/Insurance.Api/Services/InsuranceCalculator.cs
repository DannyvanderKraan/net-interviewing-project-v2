using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Services
{
    public class InsuranceCalculator : IInsuranceCalculator
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISurchargeService _surchargeService;

        public InsuranceCalculator(IProductRepository productRepository,
            IProductTypeRepository productTypeRepository,
            ISurchargeService surchargeService)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _surchargeService = surchargeService;
        }

        /// <inheritdoc/>
        public virtual InsuranceDto Calculate(int productId)
        {
            var product = _productRepository.GetProduct(productId);
            var productType = _productTypeRepository.GetProductType(product.ProductTypeId);
            var surCharge = _surchargeService.GetSurcharge(productType.Id);

            var toInsure = new InsuranceDto()
            {
                InsuranceValue = surCharge == null ? 0 : surCharge.Surcharge,
                ProductId = productId,
                ProductTypeHasInsurance = productType.CanBeInsured,
                ProductTypeName = productType.Name,
                SalesPrice = product.SalesPrice
            };

            //If product type cannot be insured then insurance value is € 0
            if (!toInsure.ProductTypeHasInsurance) return toInsure;

            //If the product sales price is less than € 500, no insurance required
            if (product.SalesPrice < 500)
                //Unless it's a laptop then insurace value should be € 500
                if (toInsure.ProductTypeName == _productTypeRepository.ProductTypeNameLaptops)
                {
                    toInsure.InsuranceValue = 500;
                }
                else
                {
                    toInsure.InsuranceValue = 0;
                }
            else //The product sales price is higher than € 500
            {
                //If the product sales price => € 500 but < € 2000, insurance cost is € 1000
                if (toInsure.SalesPrice > 500 && toInsure.SalesPrice < 2000)
                    toInsure.InsuranceValue = 1000;
                //If the product sales price => € 2000, insurance cost is €2000
                if (toInsure.SalesPrice >= 2000)
                    toInsure.InsuranceValue = 2000;
                //If the type of the product is a smartphone or a laptop, add € 500 more to the insurance cost.
                if (toInsure.ProductTypeName == _productTypeRepository.ProductTypeNameLaptops || toInsure.ProductTypeName == _productTypeRepository.ProductTypeNameSmartphones)
                    toInsure.InsuranceValue += 500;
            }

            return toInsure;
        }

        /// <inheritdoc/>
        public virtual OrderDto Calculate(IEnumerable<int> productIds)
        {
            var orderToInsure = new OrderDto();
            foreach (var productId in productIds)
            {
                orderToInsure.ProductsToInsure.Add(Calculate(productId));
            }
            //Calculate total insurance value
            orderToInsure.InsuranceValue = orderToInsure.ProductsToInsure.Sum(p => p.InsuranceValue);
            //If an order has one or more digital cameras, add € 500 to the insured value of the order.
            if (orderToInsure.ProductsToInsure.Any(p => p.ProductTypeName == _productTypeRepository.ProductTypeNameDigitalCameras)) orderToInsure.InsuranceValue += 500;
            return orderToInsure;
        }
    }
}
