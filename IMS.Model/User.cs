using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Model
{
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 单位Id
        /// </summary>
        public string Oid { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Uid { get; set; }
        public string Pwd { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string RegDate { get; set; }
        public string Privilege { get; set; }
        public string Status { get; set; }
    }
}
