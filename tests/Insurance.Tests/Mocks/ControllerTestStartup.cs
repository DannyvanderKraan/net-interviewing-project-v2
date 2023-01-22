using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Insurance.Tests.Mocks
{
    public class ControllerTestStartup
    {
        //Product Ids
        public const int ProductIdWithSalesPricesBetween500And2000 = 1;
        public const int ProductIdLaptopWithSalesPricesLowerThan500 = 2;
        public const int ProductIdSmartphoneWithSalesPricesLowerThan500 = 3;
        public const int ProductIdLaptopWithSalesPricesHigherThan500 = 4;
        public const int ProductIdSmartphoneWithSalesPricesHigherThan500 = 5;
        public const int ProductIdSalesPricesHigherThan2000 = 6;
        public const int ProductIdWithSalesPricesBetween500And2000NotInsured = 7;
        public const int ProductIdLaptopWithSalesPricesLowerThan500NotInsured = 8;
        public const int ProductIdSmartphoneWithSalesPricesLowerThan500NotInsured = 9;
        public const int ProductIdLaptopWithSalesPricesHigherThan500NotInsured = 10;
        public const int ProductIdSmartphoneWithSalesPricesHigherThan500NotInsured = 11;
        public const int ProductIdSalesPricesHigherThan2000NotInsured = 12;
        public const int ProductIdDigitalCamerasWithSalesPricesLowerThan500 = 13;

        //Product Type Ids
        public const int ProductTypeIdGeneric = 1;
        public const int ProductTypeIdLaptops = 2;
        public const int ProductTypeIdSmartphones = 3;
        public const int ProductTypeIdNotInsured = 4;
        public const int ProductTypeIdDigitalCameras = 5;


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
                            if (productId == ProductIdLaptopWithSalesPricesLowerThan500)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Lenovo Chromebook C330-11 81HY000MMH",
                                    productTypeId = ProductTypeIdLaptops,
                                    salesPrice = 299
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSmartphoneWithSalesPricesLowerThan500)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Huawei P30 Lite 128 GB Black",
                                    productTypeId = ProductTypeIdSmartphones,
                                    salesPrice = 219
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSalesPricesHigherThan2000)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Canon EOS 5D Mark IV Body",
                                    productTypeId = ProductTypeIdGeneric,
                                    salesPrice = 2699
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdLaptopWithSalesPricesHigherThan500)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Lenovo IdeaPad L340-15IRH Gaming 81LK01FUMH",
                                    productTypeId = ProductTypeIdLaptops,
                                    salesPrice = 779
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSmartphoneWithSalesPricesHigherThan500)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "OnePlus 8 Pro 128GB Black 5G",
                                    productTypeId = ProductTypeIdSmartphones,
                                    salesPrice = 886
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdWithSalesPricesBetween500And2000NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Test product between 500 and 2000 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 750
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdLaptopWithSalesPricesHigherThan500NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Laptop higher than 500 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 779
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdLaptopWithSalesPricesLowerThan500NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Laptop lower than 500 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 299
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSalesPricesHigherThan2000NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Test product higher than 2000 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 2699
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSmartphoneWithSalesPricesHigherThan500NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Smartphone higher than 500 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 886
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdSmartphoneWithSalesPricesLowerThan500NotInsured)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Smartphone lower than 500 not insured",
                                    productTypeId = ProductTypeIdNotInsured,
                                    salesPrice = 219
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            if (productId == ProductIdDigitalCamerasWithSalesPricesLowerThan500)
                            {
                                var product = new
                                {
                                    id = productId,
                                    name = "Digital camera lower than 500",
                                    productTypeId = ProductTypeIdDigitalCameras,
                                    salesPrice = 499
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                            };
                            //Return product with Id 1 (ProductIdWithSalesPricesBetween500And2000) as default
                            var defaultProduct = new
                            {
                                id = productId,
                                name = "Test Product",
                                productTypeId = ProductTypeIdGeneric,
                                salesPrice = 750
                            };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(defaultProduct));
                        }
                    );
                    ep.MapGet(
                        "product_types/{id:int}",
                        context =>
                        {
                            int productTypeId = int.Parse((string)context.Request.RouteValues["id"]);

                            if (productTypeId == ProductTypeIdLaptops)
                            {
                                var productType = new
                                {
                                    id = productTypeId,
                                    name = "Laptops",
                                    canBeInsured = true
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(productType));
                            };
                            if (productTypeId == ProductTypeIdSmartphones)
                            {
                                var productType = new
                                {
                                    id = productTypeId,
                                    name = "Smartphones",
                                    canBeInsured = true
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(productType));
                            };
                            if (productTypeId == ProductTypeIdNotInsured)
                            {
                                var productType = new
                                {
                                    id = productTypeId,
                                    name = "Test type can't be insured",
                                    canBeInsured = false
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(productType));
                            };
                            if (productTypeId == ProductTypeIdDigitalCameras)
                            {
                                var productType = new
                                {
                                    id = productTypeId,
                                    name = "Digital cameras",
                                    canBeInsured = true
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(productType));
                            };

                            //Return product type with Id 1 as default
                            var defaultProductType = new
                            {
                                id = 1,
                                name = "Test type can be insured",
                                canBeInsured = true
                            };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(defaultProductType));
                        }
                    );
                }
            );
        }
    }
}