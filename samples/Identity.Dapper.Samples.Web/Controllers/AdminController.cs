using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Identity.Dapper.Samples.Web.Model;
using Identity.Dapper.Samples.Web.Controllers.Interface;
using Identity.Dapper.Samples.Web.Repository;
using Newtonsoft.Json;
using Identity.Dapper.Connections;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Dapper.Samples.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
