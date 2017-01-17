using DemoProject.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Controllers.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// Add Product
        /// </summary>
        /// <returns></returns>
        int Add(Product prod);

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns>A list of Products</returns>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// To delete Product
        /// </summary>
        /// <returns></returns>
        void Delete(int? id);

        /// <summary>
        /// Update Product
        /// </summary>
        /// <returns></returns>
        void Update(Product prod);
    }
}
