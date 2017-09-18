using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;

namespace API.Controllers
{
    /// <summary>
    /// 菜单 API
    /// </summary>
    public class MenusController : ApiController
    {
        MenuBLL menuBLL = new MenuBLL();

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        [HttpGet, Route("menus")]
        public Result<List<MenuDto>> Get(int? parentId = null)
        {
            Result<List<MenuDto>> result = new Result<List<MenuDto>>();
            try
            {
                List<MenuDto> menuDtoList = menuBLL.getList(parentId);
                result.succeed(menuDtoList);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}