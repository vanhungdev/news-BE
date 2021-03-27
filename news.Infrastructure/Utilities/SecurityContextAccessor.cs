using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace news.Infrastructure.Utilities
{
    public static partial class Helper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static HttpContext _context => _httpContextAccessor.HttpContext;

        public static void ConfigureContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetToken()
        {
            var accessToken = _context.GetTokenAsync("access_token")
                .GetAwaiter()
                .GetResult();
            return accessToken;
        }

        public static string GetDeviceIMEI() => _context.User?.FindFirstValue(Consts.Consts.CLAIM_DEVICE_IMEI) ?? string.Empty;

        public static string GetUserName() => "HCM.conglt16";

        public static string GetHeaderByKey(string key)
        {
            if (_context.Request.Headers.TryGetValue(key, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public static bool IsAuthenticated
        {
            get
            {
                var isAuthenticated = _context?.User?.Identities?.FirstOrDefault()?.IsAuthenticated;
                if (!isAuthenticated.HasValue)
                {
                    return false;
                }

                return isAuthenticated.Value;
            }
        }

        public static void SetCustomLog(string content)
        {
            string currentLog = string.Empty;
            if (_context != null)
            {
                try
                {
                    if (_context.Items["CustomLog"] != null)
                    {
                        currentLog = _context.Items["CustomLog"].ToString();
                        _context.Items.Remove("CustomLog");
                        currentLog += content + "\r\n";
                    }
                    else
                    {
                        currentLog += content + "\r\n";
                    }
                    _context.Items.Add("CustomLog", currentLog);
                }
                catch
                {
                    _context.Items.Remove("CustomLog");
                }
            }
        }

        public static string GetCustomLog()
        {
            if (_context != null)
            {
                try
                {
                    if (_context.Items["CustomLog"] != null)
                    {
                        return _context.Items["CustomLog"].ToString();
                    }
                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
