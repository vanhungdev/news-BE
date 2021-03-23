using System;
using System.Collections.Generic;
using System.Linq;
using news.Infrastructure.Enums;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Auth
{
 
    public class JwtAuthResult
    {

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
        public JwtAuthResult()
        {

        }
        public JwtAuthResult(string AccessToken, string RefreshToken)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
        }
    }
    public class InfoUser
    {
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceIMEI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsRefreshToken { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Checksum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public UserRole UserType { set; get; } = UserRole.Normal;

        /// <summary>
        /// mô tả 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 1: success 0: fail
        /// </summary>
        public int Code { get; set; }
    }
}

