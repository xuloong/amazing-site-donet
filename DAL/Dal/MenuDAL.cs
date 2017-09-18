using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class MenuDAL : ORMBase<DbEntities>
    {
        public List<Menu> getList(int? parentId)
        {
            Expression<Func<Menu, bool>> predicate = PredicateExtensionses.True<Menu>();
            predicate = predicate.And(m => m.DeleteFlag == "N");
            if (parentId != null && parentId != 0)
            {
                predicate = predicate.And(m => m.ParentId == parentId);
            }
            return db.Menu.Where(predicate).OrderBy(m => m.OrderByNum).ToList<Menu>();
        }

        public Menu getById(int id)
        {
            return db.Menu.Where(M => M.Id == id).FirstOrDefault();
        }

        public int insert(Menu menu)
        {
            menu.CreateTime = DateTime.Now;
            menu.DeleteFlag = "N";
            db.Menu.Add(menu);
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int update(Menu menu)
        {
            menu.UpdateTime = DateTime.Now;
            db.Menu.Attach(menu);
            db.Entry(menu).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int delete(int id, int deleteUserId)
        {
            Menu menu = this.getById(id);
            if (menu != null)
            {
                menu.DeleteFlag = "Y";
                menu.DeleteUserId = deleteUserId;
                menu.DeleteTime = DateTime.Now;
                return db.SaveChanges() > 0 ? 1 : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
