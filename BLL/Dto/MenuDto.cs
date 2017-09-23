using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> ArticleId { get; set; }
        public Nullable<int> OrderByNum { get; set; }
        public List<MenuDto> subMenu { get; set; }
    }
}
