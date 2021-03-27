using Microsoft.IdentityModel.Tokens;
using news.Infrastructure.Consts;
using news.Infrastructure.Database;
using news.Infrastructure.Enums;
using news.Infrastructure.Utilities;
using news_API.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace news_API.Infrastructure.Auth
{
    public static class JwtAuthManager
    {
        /// <summary>
        /// GenerateTokens
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="deviceIMEI"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static JwtAuthResult GenerateTokens(AuthRequest user, UserRole role = UserRole.Normal)
        {
            JwtAuthResult result = new JwtAuthResult();
            try
            {
                Claim[] claims = null;
                if (role == UserRole.Normal)
                {
                    claims = new Claim[]
                    {
                         new Claim("Username",user.Username),                       
                    };
                }
                else
                {
                    claims = new Claim[]
                    {
                        new Claim("Username",user.Username),

                        new Claim(ClaimTypes.Role,((int) Enum.Parse(typeof(UserRole), role.ToString())).ToString()),
                        
                    };
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(1000),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(Encoding.ASCII.GetBytes("abc1234567899sdasdfsdgsdg")))), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                var refreshToken = GenerateRefreshToken( user, accessToken,UserRole.Normal);

                result.AccessToken = accessToken;
                result.RefreshToken = refreshToken;
            }
            catch (Exception ex)
            {
                //Helper.SetCustomLog(string.Concat("Exception: ", ex.ToString()));
                throw;
            }

            return result;
        }


        /// <summary>
        /// GenerateRefreshToken
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="deviceIMEI"></param>
        /// <param name="accessToken"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        private static string GenerateRefreshToken(AuthRequest user, string accessToken, UserRole role = UserRole.Normal)
        {
            try
            {
                string JwtTokenChecksum = Helper.GetChecksum(accessToken, "abc1234567899sdasdfsdgsdg");
                Claim[] claims = null;
                if (role == UserRole.Normal)
                {
                    claims = new Claim[]
                    {
                         new Claim("UserName",user.Username),
                         new Claim("Checksum",JwtTokenChecksum),
                         new Claim("IsRefreshToken","true")
                    };
                }
                else
                {
                    claims = new Claim[]
                    {
                        new Claim("UserName",user.Username),
                         new Claim("Checksum",JwtTokenChecksum),
                         new Claim("IsRefreshToken","true")
                    };
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(10000),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(Encoding.ASCII.GetBytes("abc1234567899sdasdfsdgsdg")))), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var refreshToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                return refreshToken;
            }
            catch (Exception ex)
            {
                //Helper.SetCustomLog(string.Concat("Exception: ", ex.ToString()));
                throw;
            }
        }
        /// <summary>
        /// RefreshTokenValidation
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static InfoUser RefreshTokenValidation(string accessToken, string refreshToken)
        {
            var infoUser = new InfoUser();
            try
            {
                ClaimsPrincipal principal = null;
                try
                {
                    principal = new JwtSecurityTokenHandler()
                        .ValidateToken(refreshToken,
                            new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(Encoding.ASCII.GetBytes("abc1234567899sdasdfsdgsdg")))),
                                ValidateLifetime = true,
                                ClockSkew = TimeSpan.Zero
                            },
                            out var validatedToken);
                }
                catch (SecurityTokenExpiredException)
                {
                    infoUser.Code = -1;
                    infoUser.Description = "Phiên làm việc của bạn đã kết thúc. Vui lòng đăng nhập lại";
                }
                catch
                {
                    principal = null;
                }

                var infoClaims = GetInfoClaims<InfoUser>(principal?.Claims);
                string JwtTokenChecksum = Helper.GetChecksum(accessToken, "abc1234567899sdasdfsdgsdg");

                if (principal == null || infoClaims == null)
                {
                    infoUser.Code = -1;
                    infoUser.Description = "Error validate refreshToken";
                    return infoUser;
                }

                if (!infoClaims.IsRefreshToken.Equals("true")
                    || !infoClaims.Checksum.Equals(JwtTokenChecksum))
                {
                    infoUser.Code = -1;
                    infoUser.Description = "RefreshToken invalid";
                    return infoUser;
                }

                infoUser = infoClaims;
                infoUser.Code = 1;
            }
            catch (Exception ex)
            {
                //Helper.SetCustomLog(string.Concat("Exception: ", ex.ToString()));
                return null;
            }
            return infoUser;
        }
        public static T GetInfoClaims<T>(IEnumerable<Claim> claims)
        {
            try
            {
                if (claims != null && claims.Any())
                {
                    var instance = Activator.CreateInstance<T>();
                    foreach (var pro in instance.GetType().GetProperties())
                    {
                        var claim = claims.FirstOrDefault(x => x.Type == pro.Name);
                        if (claim != null)
                        {
                            if (pro.PropertyType == typeof(UserRole))
                            {
                                var val = int.Parse(claim.Value);
                                pro.SetValue(instance, val, null);
                            }
                            else
                                pro.SetValue(instance, claim.Value, null);
                        }
                    }
                    return instance;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// AddTokenToBlacklist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        public static void AddTokenToBlacklist(string userName, string accessToken, string refreshToken)
        {
            try
            {
                string key = string.Format(CacheKeys.AUTHEN, userName.ToUpper());
                RedisDb.saveTokenToBlackRedis(key, accessToken);
            }
            catch (Exception ex)
            {
                //Helper.SetCustomLog(string.Concat("Exception: ", ex.ToString()));
            }
        }

        /// <summary>
        /// IsExistInBlacklist
        /// </summary>
        /// <param name = "userName" ></ param >
        /// < param name="accessToken"></param>
        /// <param name = "refreshToken" ></ param >
        public static bool IsExistInBlacklist(string userName, string accessToken, string refreshToken)
        {
            try
            {
                string key = string.Format(CacheKeys.AUTHEN, userName.ToUpper());
                var backlist = RedisDb.SortedSetRange(key);
                return backlist.Any(x => x.Equals(accessToken ?? "") || x.Equals(refreshToken ?? ""));
            }
            catch (Exception ex)
            {
                // TODO
                return false;
            }
        }
    }

}
