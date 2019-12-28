using Project.Common.DataConvert;
using Project.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Appsettings
{
    /// <summary>
    /// 初始化数据库连接字符串
    /// </summary>
    public class DBConfigHelper
    {
        private static string sqlServerConnection = AppSettingsHelper.app(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnabled = (AppSettingsHelper.app(new string[] { "AppSettings", "SqlServer", "Enabled" })).ObjToBool();

        private static string mySqlConnection = AppSettingsHelper.app(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnabled = (AppSettingsHelper.app(new string[] { "AppSettings", "MySql", "Enabled" })).ObjToBool();

        private static string oracleConnection = AppSettingsHelper.app(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool IsOracleEnabled = (AppSettingsHelper.app(new string[] { "AppSettings", "Oracle", "Enabled" })).ObjToBool();

        public static string ConnectionString => InitConn();
        public static DataBaseType DBType = DataBaseType.SqlServer;

        private static string InitConn()
        {
            if (isSqlServerEnabled)
            {
                DBType = DataBaseType.SqlServer;
                return sqlServerConnection;
            }
            else if (isMySqlEnabled)
            {
                DBType = DataBaseType.MySql;
                return mySqlConnection;
            }
            else if (IsOracleEnabled)
            {
                DBType = DataBaseType.Oracle;
                return oracleConnection;
            }
            else
            {
                return sqlServerConnection;
            }

        }
    }
}
