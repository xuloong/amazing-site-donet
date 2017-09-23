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
        public Result<List<MenuDto>> Get(int? parentId = null, string callback = "")
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

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        [HttpGet, Route("menus")]
        public Result<MenuDto> Get(int id, string callback = "")
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                MenuDto menuDto = menuBLL.getById(id);
                result.succeed(menuDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, Route("menus")]
        public Result<MenuDto> Post([FromBody]MenuDto menu, string callback = "")
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                result.succeed(menuBLL.insert(menu, int.Parse(HttpContext.Current.User.Identity.Name)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <param name="article">菜单对象</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut, Route("menus")]
        public Result<MenuDto> Put(int id, [FromBody]MenuDto menu, string callback = "")
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                menu.Id = id;
                result.succeed(menuBLL.update(menu, int.Parse(HttpContext.Current.User.Identity.Name)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns>1:删除成功;0:删除失败</returns>
        [Authorize]
        [HttpDelete, Route("menus")]
        public Result<int> Delete(int id, string callback = "")
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(menuBLL.delete(id, int.Parse(HttpContext.Current.User.Identity.Name)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}