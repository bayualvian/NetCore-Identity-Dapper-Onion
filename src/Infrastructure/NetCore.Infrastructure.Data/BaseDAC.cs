using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using NetCore.Core.Entities;
using NetCore.Core.Framework;

namespace NetCore.Infrastructure.Data
{
    public abstract class BaseDAC
    {
        private static string defaultConnection;
        private static bool _initialized;
        public BaseDAC()
        {
            if (!_initialized)
            {
                _initialized = true;
                defaultConnection = ConfigManager.GetInstance().GetConfig().ConnectionStrings.DefaultConnection;
            }
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(defaultConnection);
        }
    }
}
