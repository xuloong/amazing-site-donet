using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BannerDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类别（P:PC端;H:H5端）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageSrc { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 状态（Y:有效;N:无效）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 排序数
        /// </summary>
        public Nullable<int> OrderByNum { get; set; }
    }
}
