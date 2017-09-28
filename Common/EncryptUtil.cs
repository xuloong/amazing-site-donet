using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EncryptUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="toCryString">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString))).Replace("-", "").ToLower();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="toCryString">要加密的字符串</param>
        /// <returns></returns>
        public static string Password(string toCryString)
        {
            return MD5(MD5(toCryString + "amazing") + "site"); ;
        }
    }
}
