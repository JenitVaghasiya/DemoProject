using Identity.Dapper.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using Identity.Dapper.Models;
using Identity.Dapper.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Identity.Dapper.SqlServer.Connections
{
    public class SqlServerConnectionProvider : IConnectionProvider
    {
        private readonly IOptions<ConnectionProviderOptions> _connectionProviderOptions;
        private readonly EncryptionHelper _encryptionHelper;
        public SqlServerConnectionProvider(IOptions<ConnectionProviderOptions> connProvOpts, EncryptionHelper encHelper)
        {
            _connectionProviderOptions = connProvOpts;
            _encryptionHelper = encHelper;
        }

        public DbConnection Create()
        {
            if (_connectionProviderOptions.Value == null)
                throw new ArgumentNullException("There's no DapperIdentity configuration section registered. Please, register the section in appsettings.json or user secrets.");

            if (string.IsNullOrEmpty(_connectionProviderOptions.Value?.ConnectionString))
                throw new ArgumentNullException("There's no DapperIdentity:ConnectionString configured. Please, register the value.");

            if (string.IsNullOrEmpty(_connectionProviderOptions.Value?.Password))
                throw new ArgumentNullException("There's no DapperIdentity:Password configured. Please, register the value.");

            if (string.IsNullOrEmpty(_connectionProviderOptions.Value?.Username))
                throw new ArgumentNullException("There's no DapperIdentity:Username configured. Please, register the value.");

            var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connectionProviderOptions.Value.ConnectionString);
            sqlConnectionBuilder.Password = _encryptionHelper.TryDecryptAES256(_connectionProviderOptions.Value.Password);
            sqlConnectionBuilder.UserID = _connectionProviderOptions.Value.Username;

            return new SqlConnection(sqlConnectionBuilder.ToString());
        }
    }
}
