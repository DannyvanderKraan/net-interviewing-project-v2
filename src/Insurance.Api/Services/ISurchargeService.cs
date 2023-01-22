using Insurance.Api.Models;

namespace Insurance.Api.Services
{
    public interface ISurchargeService
    {
        /// <summary>
        /// Adds surcharge to product type
        /// </summary>
        /// <param name="surchargeDto"></param>
        void AddSurcharge(SurchargeDto surchargeDto);
        /// <summary>
        /// Gets surcharge from provided product type
        /// </summary>
        /// <param name="productTypeId">ID of product type provided</param>
        /// <returns>Data transfer object with surcharge and product type id</returns>
        SurchargeDto GetSurcharge(int productTypeId);
    }
}