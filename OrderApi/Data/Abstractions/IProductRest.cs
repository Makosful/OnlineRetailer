using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.Data.Abstractions
{
    public interface IProductRest
    {
        /// <summary>
        /// Gets a single product based on the product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product GetProductById(int productId);
    }
}