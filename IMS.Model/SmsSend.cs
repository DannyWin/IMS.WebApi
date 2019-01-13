using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Model
{
    public class SmsSend
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Key]
        public int Id { get;}

        public int Uid { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime SendTime { get; set; }

        public string Attachment { get; set; }

        public bool Delete { get; set; }
    }
}