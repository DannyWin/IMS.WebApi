using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Model.Model
{
    public class Course
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Key]
        public int Id { get; set; }

        [DisplayName("课程名称")]
        public string CourseName { get; set; }

        [DisplayName("课程代码")]
        public string CourseCode { get; set; }

        [DisplayName("专业")]
        public int Mid { get; set; }

        [DisplayName("导师")]
        public int Tid { get; set; }

        [DisplayName("类别")]
        public int Type { get; set; }

        [DisplayName("适用")]//g s y
        public string ApplyTo { get; set; }

        [DisplayName("简介")]
        public string Introduce { get; set; }

        [DisplayName("学分")]
        public int Credit { get; set; }

        [DisplayName("文档")]
        public int Doc { get; set; }

        [DisplayName("课件")]
        public int Ppt { get; set; }

        [DisplayName("视频")]
        public int Video { get; set; }

        [DisplayName("题库")]
        public int Mdb { get; set; }


    }
}