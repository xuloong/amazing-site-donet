using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;

namespace API.Controllers
{
    public class ArticlesController : ApiController
    {
        ArticleBLL articleBLL = new ArticleBLL();

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        [HttpGet, Route("articles")]
        public Result<List<ArticleDto>> Get(int pageSize, int pageIndex, string keywords = "", string callback = "")
        {
            Result<List<ArticleDto>> result = new Result<List<ArticleDto>>();
            try
            {
                int total;
                List<ArticleDto> articleDtoList = articleBLL.getPageList(pageSize, pageIndex, out total, keywords);
                result.Total = total;
                result.succeed(articleDtoList);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        [HttpGet, Route("articles")]
        public Result<ArticleDto> Get(int id, string callback = "")
        {
            Result<ArticleDto> result = new Result<ArticleDto>();
            try
            {
                ArticleDto articleDto = articleBLL.getById(id);
                result.succeed(articleDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article">文章对象</param>
        /// <returns></returns>
        [HttpPost, Route("articles")]
        public Result<ArticleDto> Post([FromBody]ArticleDto article, string callback = "")
        {
            Result<ArticleDto> result = new Result<ArticleDto>();
            try
            {
                result.succeed(articleBLL.insert(article, 1));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="article">文章对象</param>
        /// <returns></returns>
        [HttpPut, Route("articles")]
        public Result<ArticleDto> Put(int id, [FromBody]ArticleDto article, string callback = "")
        {
            Result<ArticleDto> result = new Result<ArticleDto>();
            try
            {
                article.Id = id;
                result.succeed(articleBLL.update(article, 1));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns>1:删除成功;0:删除失败</returns>
        [HttpDelete, Route("menus")]
        public Result<int> Delete(int id, string callback = "")
        {
            Result<int> result = new Result<int>();
            try
            {
                result.succeed(articleBLL.delete(id, 1));
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}