using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Model
{
    public class Article
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Key]
        public int ID { get; set; }

        [DisplayName("栏目")]
        public int NodeID { get; set; }

        [DisplayName("标题")]
        [Required]
        public string Title { get; set; }

        [DisplayName("关键字")]
        public string KeyWords { get; set; }

        [DisplayName("摘要")]
        [MaxLength(200)]
        public string Abstract { get; set; }

        [DisplayName("发布人")]
        public string Author { get; set; }

        [DisplayName("发布时间")]
        public DateTime ReleaseTime { get; set; }

        [DisplayName("点击量")]
        public int Hit { get; set; }

        [DisplayName("内容")]
        [Required]
        public string Content { get; set; }

        [DisplayName("主页图片")]
        public string HomePagePic { get; set; }

        [DisplayName("附件")]
        public string Attachment { get; set; }

        public int BePaged { get; set; }

        public int LengthPerPage { get; set; }

        public int BeApproved { get; set; }

        public int Delete { get; set; }

    }
}