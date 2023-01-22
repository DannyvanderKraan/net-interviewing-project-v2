using Insurance.Api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Insurance.Api.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ILogger<ProductTypeRepository> _logger;
        private readonly ProductAPISettings _productAPISettings;

        public ProductTypeRepository(ILogger<ProductTypeRepository> logger,
            IOptions<ProductAPISettings> productAPISettings)
        {
            _logger = logger;
            _productAPISettings = productAPISettings.Value;
        }

        /// <inheritdoc/>
        public string ProductTypeNameLaptops => "Laptops";

        /// <inheritdoc/>
        public string ProductTypeNameSmartphones => "Smartphones";

        /// <inheritdoc/>
        public string ProductTypeNameDigitalCameras => "Digital cameras";

        /// <inheritdoc/>
        public virtual ProductTypeDto GetProductType(int productTypeID)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(_productAPISettings.Url) };
                string json = client.GetAsync(string.Format("/product_types/{0:G}", productTypeID)).Result.Content.ReadAsStringAsync().Result;
                var productType = JsonConvert.DeserializeObject<ProductTypeDto>(json);
                return productType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, default);
                throw;
            }
        }

    }
}
