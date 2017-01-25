using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Identity.Dapper.Samples.Web.Model;
using Identity.Dapper.Samples.Web.Controllers.Interface;
using Identity.Dapper.Samples.Web.Repository;
using Newtonsoft.Json;
using Identity.Dapper.Connections;

namespace Identity.Dapper.Samples.Web.Controllers
{
    [Route("Products")]
    //[Authorize(Roles ="admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _Repo;
        public ProductsController(IConnectionProvider connProv)
        {
            _Repo = new ProductRepository(connProv);
            //_Repo = new ProductRepository();
        }

        // GET: Products
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("list")]
        [HttpGet]
        public JsonResult Productlist()
        {
            var objreturn = _Repo.GetAll();
            return Json(objreturn);
        }


        [Route("Update")]
        [HttpPost]
        public Model.Product ProductUpdate(string models)
        {
            Product UpdateProduct = new Product();
            if (ModelState.IsValid)
            {
                UpdateProduct = JsonConvert.DeserializeObject<List<Product>>(models).FirstOrDefault();
                _Repo.Update(UpdateProduct);
                
            }

            return UpdateProduct;
        }

        [Route("Delete")]
        [HttpPost]
        public Model.Product ProductDelete(string models)
        {
            Product DeleteProduct = new Product();
            if (ModelState.IsValid)
            {
                DeleteProduct = JsonConvert.DeserializeObject<List<Product>>(models).FirstOrDefault();
                _Repo.Delete(DeleteProduct.productId);

            }

            return DeleteProduct;
        }

        [Route("Create")]
        [HttpPost]
        public Model.Product ProductCreate(string models)
        {
            Product AddProduct = new Product();
            if (ModelState.IsValid)
            {
                AddProduct = JsonConvert.DeserializeObject<List<Product>>(models).FirstOrDefault();
                AddProduct.productId = _Repo.Add(AddProduct);
            }

            return AddProduct;
        }

    }
}
