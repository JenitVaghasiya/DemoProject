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

                //var p = new DynamicParameters(new { Name = prod.name, Quantity = prod.quantity, Price = prod.price });
                //p.Add("@RESULT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //p.Add("@rval", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                //dbConnection.Execute("InsertProduct", p, commandType: CommandType.StoredProcedure);
                //return p.Get<int>("@RESULT");
                return 1;

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
                string sQuery = "DELETE FROM Products"
                             + " WHERE ProductId = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Employee emp)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE Products SET Name = @Name,"
                               + " Quantity = @Quantity, Price= @Price"
                               + " WHERE ProductId = @ProductId";
                dbConnection.Open();
                dbConnection.Query(sQuery, emp);
            }
        }
    }
}
