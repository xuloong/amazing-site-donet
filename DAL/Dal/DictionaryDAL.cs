using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class DictionaryDAL : ORMBase<DbEntities>
    {
        public Dictionary getByKey(string key)
        {
            return db.Dictionary.Where(M => M.Key == key).FirstOrDefault();
        }

        public int update(Dictionary dictionary)
        {
            db.Dictionary.Attach(dictionary);
            db.Entry(dictionary).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? 1 : 0;
        }
    }
}
