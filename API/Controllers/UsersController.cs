using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;
using System.Net.Http.Headers;
using System.Net.Http;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        UserBLL userBLL = new UserBLL();

        /// <summary>
        /// 获取当前用户详情
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet, Route("api/users/me")]
        public Result<UserDto> Get(string callback = "")
        {
            Result<UserDto> result = new Result<UserDto>();
            try
            {
                UserDto userDto = userBLL.getById(LoginInfo.getUserId(Request.Headers));
                result.succeed(userDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPatch, Route("api/users/me/password")]
        public Result<int> PatchPassword([FromBody]ChangePasswordDto changePasswordDto, string callback = "")
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(userBLL.changePassword(LoginInfo.getUserId(Request.Headers), changePasswordDto.Password, changePasswordDto.NewPassword, LoginInfo.getUserId(Request.Headers)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet, Route("api/users/login")]
        public Result<UserDto> Login(string username, string password, string callback = "")
        {
            Result<UserDto> result = new Result<UserDto>();
            try
            {
                UserDto userDto = userBLL.login(username, password);
                if (userDto == null)
                    result.fail("username or password is incorrect");
                else
                    result.succeed(userDto);

                //var cookie = new HttpCookie("token", userDto.Token);
                //cookie.Expires = DateTime.Now.AddDays(1);
                //cookie.Domain = Request.RequestUri.Host;
                //cookie.Path = "/";
                //HttpContext.Current.Response.Cookies.Add(cookie);

            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}