using Identity.Dapper.Samples.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Dapper.Samples.Web.Repository.Interface
{
    interface IAdminRepository
    {
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>
        int Add(Employee emp);

        /// <summary>
        /// Get all Employee
        /// </summary>
        /// <returns>A list of Products</returns>
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <returns>A list of Departments</returns>
        List<object> GetDepartmentAll();

        /// <summary>
        /// To delete Employee
        /// </summary>
        /// <returns></returns>
        void Delete(int? id);

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <returns></returns>
        void Update(Employee emp);
    }
}
