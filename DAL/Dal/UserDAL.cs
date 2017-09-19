using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class UserDAL : ORMBase<DbEntities>
    {
        public List<User> getPageList(int pageSize, int pageIndex, out int total, string keywords)
        {
            Expression<Func<User, bool>> predicate = PredicateExtensionses.True<User>();
            predicate = predicate.And(m => m.DeleteFlag == "N");
            if (!string.IsNullOrEmpty(keywords))
            {
                predicate = predicate.And(m => m.Name.Contains(keywords));
            }
            total = db.User.Where(predicate).Count();
            return db.User.Where(predicate).OrderByDescending(m => m.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<User>();
        }

        public User getById(int id)
        {
            return db.User.Where(M => M.Id == id).FirstOrDefault();
        }

        public User getByUsername(string username)
        {
            return db.User.Where(M => M.Username == username).FirstOrDefault();
        }

        public int insert(User user)
        {
            user.CreateTime = DateTime.Now;
            user.DeleteFlag = "N";
            db.User.Add(user);
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int update(User user)
        {
            user.UpdateTime = DateTime.Now;
            db.User.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int delete(int id, int deleteUserId)
        {
            User user = this.getById(id);
            if (user != null)
            {
                user.DeleteFlag = "Y";
                user.DeleteUserId = deleteUserId;
                user.DeleteTime = DateTime.Now;
                return db.SaveChanges() > 0 ? 1 : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
