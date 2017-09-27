using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;


namespace API.Controllers
{
    public class DictionarysController : ApiController
    {
        DictionaryBLL dictionaryBLL = new DictionaryBLL();

        /// <summary>
        /// 获取字典详情
        /// </summary>
        /// <param name="key">字典key</param>
        /// <returns></returns>
        [HttpGet, Route("api/dictionarys/{key}")]
        public Result<DictionaryDto> Get(string key, string callback = "")
        {
            Result<DictionaryDto> result = new Result<DictionaryDto>();
            try
            {
                DictionaryDto dictionaryDto = dictionaryBLL.getByKey(key);
                result.succeed(dictionaryDto);
            }
            catch (Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}