using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using System.Web;
using System.Net.Http.Headers;

namespace API.Controllers
{
    /// <summary>
    /// Banner API
    /// </summary>
    public class BannersController : ApiController
    {
        BannerBLL bannerBLL = new BannerBLL();

        /// <summary>
        /// 获取Banner列表
        /// </summary>
        /// <param name="type">类别（P:PC端;H:H5端）</param>
        /// <param name="status">状态（Y:有效;N:无效）</param>
        /// <returns></returns>
        [HttpGet, Route("api/banners")]
        public Result<List<BannerDto>> Get(string type = "", string status = "", string callback = "")
        {            
            Result<List<BannerDto>> result = new Result<List<BannerDto>>();
            try
            {
                List<BannerDto> bannerDtoList = bannerBLL.getList(type, status);
                result.succeed(bannerDtoList);                
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
           return result;
        }

        /// <summary>
        /// 获取Banner详情
        /// </summary>
        /// <param name="id">BannerID</param>
        /// <returns></returns>
        [HttpGet, Route("api/banners/{id}")]
        public Result<BannerDto> Get(int id, string callback = "")
        {
            Result<BannerDto> result = new Result<BannerDto>();
            try
            {
                BannerDto bannerDto = bannerBLL.getById(id);
                result.succeed(bannerDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 新增Banner
        /// </summary>
        /// <param name="banner">Banner对象</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost, Route("api/banners")]
        public Result<BannerDto> Post([FromBody]BannerDto banner, string callback = "")
        {
            Result<BannerDto> result = new Result<BannerDto>();
            if (LoginInfo.Unauthorized(Request.Headers.Authorization))
            {
                result.unauthorized();
                return result;
            }                 
            try
            {
                result.succeed(bannerBLL.insert(banner, LoginInfo.getUserId(Request.Headers.Authorization)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改Banner
        /// </summary>
        /// <param name="id">BannerID</param>
        /// <param name="banner">Banner对象</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut, Route("api/banners/{id}")]
        public Result<BannerDto> Put(int id, [FromBody]BannerDto banner, string callback = "")
        {
            Result<BannerDto> result = new Result<BannerDto>();
            if (LoginInfo.Unauthorized(Request.Headers.Authorization))
            {
                result.unauthorized();
                return result;
            }
            try
            {
                banner.Id = id;
                result.succeed(bannerBLL.update(banner, LoginInfo.getUserId(Request.Headers.Authorization)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除Banner
        /// </summary>
        /// <param name="id">BannerID</param>
        /// <returns>1:删除成功;0:删除失败</returns>
        //[Authorize]
        [HttpDelete, Route("api/banners/{id}")]
        public Result<int> Delete(int id, string callback = "")
        {
            Result<int> result = new Result<int>();
            if (LoginInfo.Unauthorized(Request.Headers.Authorization))
            {
                result.unauthorized();
                return result;
            }
            try
            {
                result.succeed(bannerBLL.delete(id, LoginInfo.getUserId(Request.Headers.Authorization)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

    }
}
