using System;
using System.Collections.Generic;
using System.Text;
using static news.Infrastructure.Configuration.AppSettingDetail;

namespace news.Infrastructure.Configuration
{
    public class AppSettings
    {
        public CommonSettings CommonSettings { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
        public string AppName { get; set; }
    }
    public class AppSettingDetail
    {
        public class CommonSettings
        {
            public string StaticStorage { get; set; }

            public int IsMaintain { get; set; }
        }
        public class RedisSettings
        {
            public string ServerWrite { get; set; }
            public string ServerRead { get; set; }
            public int DatabaseNumber { get; set; }
        }
        public class ConnectionStringSettings
        {
            public string SqlServerConnectString { get; set; }
        }
        public class SecuritySettings
        {
            public string PartnerKey { get; set; }

            public string ChecksumKey { get; set; }

            public int EnableCheckSum { get; set; }

            public string JwtSecretKey { get; set; }

            public int AccessTokenExpire { get; set; }

            public int RefreshTokenExpire { get; set; }
        }
    }
}
