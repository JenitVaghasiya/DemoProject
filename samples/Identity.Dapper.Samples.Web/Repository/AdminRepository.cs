using Identity.Dapper.Samples.Web.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Identity.Dapper.Samples.Web.Models;
using Identity.Dapper.Connections;
using Identity.Dapper.Samples.Web.Repository.Interface;
using System.Reflection;
using System.ComponentModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Dapper.Samples.Web.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public AdminRepository(IConnectionProvider connProv)
        {
            _connectionProvider = connProv;
        }

        public IDbConnection Connection
        {
            get
            {
                return _connectionProvider.Create();
            }
        }

        public int Add(Employee emp)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                //Using Store Procedure.

                var p = new DynamicParameters(new { EmpName = emp.empName, DeptName = emp.deptName, Username = emp.username, MobileNo= emp.mobileNo, JoiningDate = emp.joiningDate, RelevingDate = emp.relevingDate });
                p.Add("@RESULT", dbType: DbType.Int32, direction: ParameterDirection.Output);

                dbConnection.Execute("InsertEmployee", p, commandType: CommandType.StoredProcedure);
                return p.Get<int>("@RESULT");
    
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Employee>("SELECT * FROM Employee Where IsActive = 1");
            }
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public List<object> GetDepartmentAll()
        {
            List<object> values = new List<object>();
            foreach (var value in Enum.GetValues(typeof(Department)).Cast<Department>().ToList())
            {
                values.Add(new
                {

                    text = (value).ToString(),
                    value = (value).ToString()
                });
            }
            return values;
        }

        public int IsUserNamevalid(string eid, string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = string.Empty;
                dbConnection.Open();
                if (string.IsNullOrEmpty(eid)|| eid == "0")
                {
                    sQuery = "SELECT * FROM Employee" + " WHERE UserName = @Username";
                    return dbConnection.Query<int>(sQuery, new { Username = username }).FirstOrDefault();
                }
                else
                {
                    sQuery = "SELECT * FROM Employee" + " WHERE EmployeeID <> @employeeid AND UserName = @username";
                    return dbConnection.Query<int>(sQuery, new { employeeid = eid, Username = username }).FirstOrDefault();
                }

                
                
            }
        }
        public Product GetByID(int? id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM Products"
                               + " WHERE ProductId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public void Delete(int? id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE Employee SET IsActive = 0"
                             + " WHERE EmployeeID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Employee emp)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                //Using Store Procedure.

                var p = new DynamicParameters(new { EmployeeID = emp.employeeID, EmpName = emp.empName, DeptName = emp.deptName, Username = emp.username, MobileNo = emp.mobileNo, JoiningDate = emp.joiningDate, RelevingDate = emp.relevingDate });

                dbConnection.Execute("UpdateEmployee", p, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
