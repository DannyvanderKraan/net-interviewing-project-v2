using Insurance.Api.Models;

namespace Insurance.Api.Repositories
{
    public interface ISurchargeRepository
    {
        /// <summary>
        /// Provides the name of the container for surcharges to product types.
        /// Prefering 'container' over for instance 'table' to abstract away the underlying storage used
        /// </summary>
        string ContainerName { get; set; }
        /// <summary>
        /// Adds surcharge to product type
        /// </summary>
        /// <param name="surchargeDto">The data transfer object which contains the surcharge and product type id</param>
        void AddSurcharge(SurchargeDto surchargeDto);
        /// <summary>
        /// Gets the surcharge by the provided ID of product type.
        /// </summary>
        /// <param name="productTypeId">ID of product type.</param>
        /// <returns>Data transfer object which contains the surcharge and product type id</returns>
        SurchargeDto GetSurcharge(int productTypeId);
    }
}