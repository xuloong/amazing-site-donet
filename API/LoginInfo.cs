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
        public static int getUserId(AuthenticationHeaderValue token)
        {
            //return int.Parse(HttpContext.Current.User.Identity.Name);
            //return 1;
            try
            {
                UserDto userDto = getCurrentUser(token);
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

        public static UserDto getCurrentUser(AuthenticationHeaderValue token)
        {
            try
            {
                if (token == null)
                    return null;
                UserBLL userBLL = new UserBLL();
                return userBLL.getByToken(token.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static bool Unauthorized(AuthenticationHeaderValue token)
        {
            try
            {
                UserDto userDto = getCurrentUser(token);
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
