using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;

namespace API.Controllers
{
    public class SuggestionsController : ApiController
    {
        SuggestionBLL suggestionBLL = new SuggestionBLL();

        /// <summary>
        /// 获取建议列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("suggestions")]
        public Result<List<SuggestionDto>> Get(int pageSize, int pageIndex, string keywords = "", string callback = "")
        {
            Result<List<SuggestionDto>> result = new Result<List<SuggestionDto>>();
            try
            {
                int total;
                List<SuggestionDto> suggestionDtoList = suggestionBLL.getPageList(pageSize, pageIndex, out total, keywords);
                result.Total = total;
                result.succeed(suggestionDtoList);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取建议详情
        /// </summary>
        /// <param name="id">建议ID</param>
        /// <returns></returns>
        [HttpGet, Route("suggestions")]
        public Result<SuggestionDto> Get(int id, string callback = "")
        {
            Result<SuggestionDto> result = new Result<SuggestionDto>();
            try
            {
                SuggestionDto suggestionDto = suggestionBLL.getById(id);
                result.succeed(suggestionDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 新增建议
        /// </summary>
        /// <param name="suggestion">建议对象</param>
        /// <returns></returns>
        [HttpPost, Route("suggestions")]
        public Result<SuggestionDto> Post([FromBody]SuggestionDto suggestion, string callback = "")
        {
            Result<SuggestionDto> result = new Result<SuggestionDto>();
            try
            {
                result.succeed(suggestionBLL.insert(suggestion, 0));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除建议
        /// </summary>
        /// <param name="id">建议ID</param>
        /// <returns>1:删除成功;0:删除失败</returns>
        [Authorize]
        [HttpDelete, Route("suggestions")]
        public Result<int> Delete(int id, string callback = "")
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(suggestionBLL.delete(id, int.Parse(HttpContext.Current.User.Identity.Name)));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}