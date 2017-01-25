using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Identity.Dapper.Samples.Web.Model;
using Identity.Dapper.Samples.Web.Repository;
using Newtonsoft.Json;
using Identity.Dapper.Connections;
using Microsoft.AspNetCore.Authorization;
using Identity.Dapper.Samples.Web.Repository.Interface;
using Identity.Dapper.Samples.Web.Models;
using Microsoft.AspNetCore.Identity;
using Identity.Dapper.Entities;
using System.Threading.Tasks;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Dapper.Samples.Web.Controllers
{
    [Route("adm/emp")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _Repo;
        private readonly UserManager<DapperIdentityUser> _userManager;
        public AdminController(IConnectionProvider connProv, UserManager<DapperIdentityUser> userManager)
        {
            _Repo = new AdminRepository(connProv);
            _userManager = userManager;
            //_Repo = new ProductRepository();
        }

        [Route("")]
        [Authorize(Roles = "admin")]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Route("list")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public JsonResult Employeelist()
        {
            var objreturn = _Repo.GetAll();
            return Json(objreturn);
        }

        [Route("department")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public JsonResult DepartmentList()
        {
            var objreturn = _Repo.GetDepartmentAll();
            return Json(objreturn);
        }


        [Route("department")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<bool> usernamevalid(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        [Route("Update")]
        [HttpPost]
        public Employee EmployeeUpdate(string models)
        {
            Employee UpdateEmployee = new Employee();
            if (ModelState.IsValid)
            {
                UpdateEmployee = JsonConvert.DeserializeObject<List<Employee>>(models).FirstOrDefault();
                _Repo.Update(UpdateEmployee);

            }

            return UpdateEmployee;
        }

        [Route("Delete")]
        [HttpPost]
        public Employee EmployeeDelete(string models)
        {
            Employee DeleteEmployee = new Employee();
            if (ModelState.IsValid)
            {
                DeleteEmployee = JsonConvert.DeserializeObject<List<Employee>>(models).FirstOrDefault();
                _Repo.Delete(DeleteEmployee.EmployeeID);

            }

            return DeleteEmployee;
        }

        [Route("Create")]
        [HttpPost]
        public Employee EmployeeCreate(string models)
        {
            Employee AddEmployee = new Employee();
            if (ModelState.IsValid)
            {
                AddEmployee = JsonConvert.DeserializeObject<List<Employee>>(models).FirstOrDefault();
                AddEmployee.EmployeeID = _Repo.Add(AddEmployee);
            }

            return AddEmployee;
        }

    }
}
