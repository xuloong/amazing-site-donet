using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using BLL.Dto;
using Common;

namespace API.Controllers
{
    public class BannersController : ApiController
    {
        BannerBLL bannerBLL = new BannerBLL();

        /// <summary>
        /// 获取Banner列表
        /// </summary>
        /// <param name="type">类别（P:PC端;H:H5端）</param>
        /// <returns></returns>
        [HttpGet, Route("banners")]
        public Result<List<BannerDto>> Get(string type = "", string status = "")
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
        /// 新增Banner
        /// </summary>
        /// <param name="banner">Banner对象</param>
        /// <returns></returns>
        [HttpPost, Route("banners")]
        public Result<BannerDto> Post([FromBody]BannerDto banner)
        {
            Result<BannerDto> result = new Result<BannerDto>();
            try
            {
                result.succeed(bannerBLL.insert(banner, 1));
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
        /// <param name="id">ID</param>
        /// <param name="banner">Banner对象</param>
        /// <returns></returns>
        [HttpPut, Route("banners")]
        public Result<BannerDto> Put(int id, [FromBody]BannerDto banner)
        {
            Result<BannerDto> result = new Result<BannerDto>();
            try
            {
                banner.Id = id;
                result.succeed(bannerBLL.update(banner, 1));
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
        /// <param name="id">ID</param>
        /// <returns>1:删除成功;0:删除失败</returns>
        [HttpDelete, Route("banners")]
        public Result<int> Delete(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(bannerBLL.delete(id, 1));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

    }
}
