using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Common;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;

namespace API.Controllers
{
    public class FilesController : ApiController
    {
        /// <summary>
        /// 通过multipart/form-data方式上传文件
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpPost, Route("api/files")]
        public async Task<Result<FileDto>> PostFile()
        {
            Result<FileDto> result = new Result<FileDto>();
            if (LoginInfo.Unauthorized(Request.Headers.Authorization))
            {
                result.unauthorized();
                return result;
            }
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("/UploadFiles/");
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadFiles/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/"));
                }

                var provider = new MultipartFormDataStreamProvider(root);

                StringBuilder sb = new StringBuilder();

                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);

                    if (fileInfo.Length <= 0)
                    {
                        result.fail("请选择上传文件");
                    }
                    else if (fileInfo.Length > 5242880)
                    {
                        result.fail("上传文件大小超过5M限制");
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        
                        String fileTypes = "jpg,jpeg,png,bmp,gif";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            result.fail("图片类型不正确");
                        }
                        else
                        {
                            String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);

                            fileInfo.CopyTo(Path.Combine(root, newFileName + fileExt), true);
                            sb.Append("http://" + HttpContext.Current.Request.Url.Host + "/UploadFiles/" + newFileName + fileExt);
                            FileDto fileDto = new FileDto();
                            fileDto.Url = sb.ToString();
                            result.succeed(fileDto);
                        }
                    }
                    fileInfo.Delete();
                }
            }
            catch (System.Exception e)
            {
                result.fail(e.Message);
            }
            return result;
        }
    }
}
