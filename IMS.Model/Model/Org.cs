using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Model.Model
{
    public class Org
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Pid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
