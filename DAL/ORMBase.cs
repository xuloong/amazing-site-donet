using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.IO;

namespace DAL
{
    public class ORMBase<T> where T : DbContext, new()
    {
        protected T db;

        public ORMBase()
        {
            db = new T();
        }

        public int SaveChanges()
        {
            int result = 0;

            try
            {
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
            }

            return result;
        }

        private void WriteErrorLog(string errorDetail)
        {
            string filePath = "c:/error.log";
            Stream fs;
            Encoding encoder = Encoding.GetEncoding("utf-8");
            byte[] arrError = encoder.GetBytes(errorDetail);

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            fs = File.Open(filePath, FileMode.Append, FileAccess.Write);
            fs.Write(arrError, 0, arrError.Length);
            fs.Flush();
            fs.Close();
        }
    }
}
