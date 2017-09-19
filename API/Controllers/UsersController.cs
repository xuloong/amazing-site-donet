using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        UserBLL userBLL = new UserBLL();

        /// <summary>
        /// 获取当前用户详情
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("users/me")]
        public Result<UserDto> Get(string callback = "")
        {
            Result<UserDto> result = new Result<UserDto>();
            try
            {
                UserDto userDto = userBLL.getById(int.Parse(HttpContext.Current.User.Identity.Name));
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
        [Authorize]
        [HttpPatch, Route("users/password")]
        public Result<int> PatchPassword([FromBody]UserDto userDto, string callback = "")
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(userBLL.updatePassword(int.Parse(HttpContext.Current.User.Identity.Name), userDto.Password, int.Parse(HttpContext.Current.User.Identity.Name)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}