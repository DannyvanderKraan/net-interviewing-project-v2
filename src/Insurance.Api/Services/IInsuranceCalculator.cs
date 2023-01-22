using Insurance.Api.Models;
using System.Collections.Generic;

namespace Insurance.Api.Services
{
    public interface IInsuranceCalculator
    {
        /// <summary>
        /// Calculate the insurance value of all the products of the ids provided and calculate the total insurance value of the order.
        /// </summary>
        /// <param name="productIds">Ids of the products in the order</param>
        /// <returns></returns>
        OrderDto Calculate(IEnumerable<int> productIds);
        /// <summary>
        /// Calculate the insurance value of the product of the id provided
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <returns></returns>
        InsuranceDto Calculate(int productId);
    }
}