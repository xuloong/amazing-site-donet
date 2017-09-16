﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BannerDAL : ORMBase<DbEntities>
    {

        public List<Banner> getListByType(string type)
        {
            return db.Banner.Where(m => m.Type == type && m.Status == "Y" && m.DeleteFlag == "N").ToList<Banner>();
        }

        public Banner getById(int id)
        {
            return db.Banner.Where(M => M.Id == id).FirstOrDefault();
        }

        public int insert(Banner banner)
        {
            banner.CreateTime = DateTime.Now;
            db.Banner.Add(banner);
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int update(Banner banner)
        {
            banner.UpdateTime = DateTime.Now;
            return db.SaveChanges() > 0 ? 1 : 0;
        }

        public int delete(int id, int deleteUserId)
        {
            Banner banner = this.getById(id);
            if (banner != null)
            {
                banner.DeleteFlag = "Y";
                banner.DeleteUserId = deleteUserId;
                banner.DeleteTime = DateTime.Now;
                return db.SaveChanges() > 0 ? 1 : 0;
            }
            else
            {
                return 0;
            }
        }

    }
}