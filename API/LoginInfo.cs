using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL;
using System.Net.Http.Headers;

namespace API
{
    public static class LoginInfo
    {
        public static int getUserId(HttpRequestHeaders headers)
        {
            //return int.Parse(HttpContext.Current.User.Identity.Name);
            //return 1;
            try
            {
                UserDto userDto = getCurrentUser(headers);
                if (userDto != null)
                    return userDto.Id;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static UserDto getCurrentUser(HttpRequestHeaders headers)
        {
            try
            {
                string token;
                if (headers == null || !headers.Contains("token"))
                    return null;
                token = headers.GetValues("token").FirstOrDefault<string>().ToString();
                UserBLL userBLL = new UserBLL();
                return userBLL.getByToken(token.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static bool Unauthorized(HttpRequestHeaders headers)
        {
            try
            {
                UserDto userDto = getCurrentUser(headers);
                if (userDto != null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
