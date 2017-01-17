﻿using DemoProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using DemoProject.Controllers.Interface;
using Identity.Dapper.Samples.Web.Models;
using Identity.Dapper.Connections;

namespace Identity.Dapper.Samples.Web.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string connectionString;
        private readonly IConnectionProvider _connectionProvider;

        public ProductRepository(IConnectionProvider connProv)
        {
            //connectionString = @"Data Source=(local)\SQLEXPRESS2014;Initial Catalog=DapperDemo;Persist Security Info=True;User ID=CivicaIQUser;Password=Password00";
            _connectionProvider = connProv;
        }

        public IDbConnection Connection
        {
            get
            {
                return _connectionProvider.Create();
            }
        }

        public int Add(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO Products (Name, Quantity, Price)"
                                + " VALUES(@Name, @Quantity, @Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, prod);

                return dbConnection.Query<int>("SELECT IDENT_CURRENT('products')").FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Product>("SELECT * FROM Products");
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
                string sQuery = "DELETE FROM Products"
                             + " WHERE ProductId = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE Products SET Name = @Name,"
                               + " Quantity = @Quantity, Price= @Price"
                               + " WHERE ProductId = @ProductId";
                dbConnection.Open();
                dbConnection.Query(sQuery, prod);
            }
        }
    }
}