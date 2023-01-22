using Insurance.Api.Models;

namespace Insurance.Api.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="productID">ID of product.</param>
        /// <returns></returns>
        ProductDto GetProduct(int productID);
    }
}