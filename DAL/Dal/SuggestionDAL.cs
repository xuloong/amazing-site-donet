using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class SuggestionDAL : ORMBase<DbEntities>
    {
        public List<Suggestion> getPageList(int pageSize, int pageIndex, out int total, string keywords)
        {
            Expression<Func<Suggestion, bool>> predicate = PredicateExtensionses.True<Suggestion>();
            predicate = predicate.And(m => m.DeleteFlag == "N");
            if (!string.IsNullOrEmpty(keywords))
            {
                predicate = predicate.And(m => m.Title.Contains(keywords));
            }
            total = db.Suggestion.Where(predicate).Count();
            return db.Suggestion.Where(predicate).OrderByDescending(m => m.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<Suggestion>();
        }

        public Suggestion getById(int id)
        {
            return db.Suggestion.Where(M => M.Id == id).FirstOrDefault();
        }

        public int insert(Suggestion suggestion)
        {
            suggestion.CreateTime = DateTime.Now;
            suggestion.DeleteFlag = "N";
            db.Suggestion.Add(suggestion);
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int update(Suggestion suggestion)
        {
            suggestion.UpdateTime = DateTime.Now;
            db.Suggestion.Attach(suggestion);
            db.Entry(suggestion).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int delete(int id, int deleteUserId)
        {
            Suggestion suggestion = this.getById(id);
            if (suggestion != null)
            {
                suggestion.DeleteFlag = "Y";
                suggestion.DeleteUserId = deleteUserId;
                suggestion.DeleteTime = DateTime.Now;
                return db.SaveChanges() > 0 ? 1 : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
