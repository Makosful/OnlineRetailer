using System.Collections.Generic;
using OrderApi.Data.Abstractions;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Data
{
    public class ProductRestClient : IProductRest
    {
        // 5000 is unsecure HTTP. 5001 is secure HTTPS
        private const string ProductApi = "http://localhost:5000";

        private readonly RestClient _client;

        public ProductRestClient()
        {
            _client = new RestClient(ProductApi);
        }

        Product IProductRest.GetProductById(int productId)
        {
            var request = new RestRequest($"products/{productId}");
            var response = _client.Get<Product>(request);

            if (response.IsSuccessful)
                return response.Data;
            throw response.ErrorException;
        }
    }
}