using Insurance.Api.Models;

namespace Insurance.Api.Repositories
{
    public interface IProductTypeRepository
    {
        /// <summary>
        /// Gets product type by ID
        /// </summary>
        /// <param name="productTypeID">ID of product type</param>
        /// <returns></returns>
        ProductTypeDto GetProductType(int productTypeID);        
        /// <summary>
        /// Provided the name of the Laptops product type
        /// </summary>
        string ProductTypeNameLaptops { get; }
        /// <summary>
        /// Provided the name of the Smartphones product type
        /// </summary>
        string ProductTypeNameSmartphones { get; }
        /// <summary>
        /// Provided the name of the Cameras product type
        /// </summary>
        string ProductTypeNameDigitalCameras { get; }
    }
}