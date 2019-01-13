using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Model.Model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }

        public int Seq { get; set; }
    }
}
