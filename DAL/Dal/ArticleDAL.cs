using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class ArticleDAL : ORMBase<DbEntities>
    {
        public List<Article> getPageList(int pageSize, int pageIndex, out int total, string keywords)
        {
            Expression<Func<Article, bool>> predicate = PredicateExtensionses.True<Article>();
            predicate = predicate.And(m => m.DeleteFlag == "N");
            if (!string.IsNullOrEmpty(keywords))
            {
                predicate = predicate.And(m => m.Title.Contains(keywords));
            }
            total = db.Article.Where(predicate).Count();
            return db.Article.Where(predicate).OrderByDescending(m => m.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<Article>();
        }

        public Article getById(int id)
        {
            return db.Article.Where(M => M.Id == id).FirstOrDefault();
        }

        public int insert(Article article)
        {
            article.CreateTime = DateTime.Now;
            article.DeleteFlag = "N";
            db.Article.Add(article);
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int update(Article article)
        {
            article.UpdateTime = DateTime.Now;
            db.Article.Attach(article);
            db.Entry(article).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int delete(int id, int deleteUserId)
        {
            Article article = this.getById(id);
            if (article != null)
            {
                article.DeleteFlag = "Y";
                article.DeleteUserId = deleteUserId;
                article.DeleteTime = DateTime.Now;
                return db.SaveChanges() > 0 ? 1 : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
