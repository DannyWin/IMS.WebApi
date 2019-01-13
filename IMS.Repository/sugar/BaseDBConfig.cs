using System;
using System.Collections.Generic;
using System.Text;
using IMS.Common;

namespace IMS.Repository
{
    public class BaseDBConfig
    {
        public static string ConnectionString { get; set; }

        //正常格式是

        //public static string ConnectionString = "server=.;uid=sa;pwd=sa;database=BlogDB"; 

        //public static string ConnectionString = Appsettings.app(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });//获取连接字符串

    }
}
