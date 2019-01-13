using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Model.Model
{
    public class SmsReceive
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Key]
        public int Id { get; set; }
        //SendID
        public int Sid { get; set; }
        public int Uid { get; set; }
        public bool Read { get; set; }
        public DateTime ReadTime { get; set; }

        public bool Delete { get; set; }
    }
}