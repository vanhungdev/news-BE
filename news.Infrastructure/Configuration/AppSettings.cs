using System;
using System.Collections.Generic;
using System.Text;

namespace news.Infrastructure.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class AppSettingDetail
    {
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
