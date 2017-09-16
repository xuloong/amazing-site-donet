using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// 返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 返回码（1:成功;0:失败）
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据集
        /// </summary>
        public T Data { get; set; }
    
        public void succeed()
        {
            this.Code = 1;
            this.Message = "success";
        }

        public void succeed(T t)
        {
            this.Code = 1;
            this.Message = "success";
            this.Data = t;
        }

        public void fail(string message)
        {
            this.Code = 0;
            this.Message = "error:" + message;
        }

    }
}