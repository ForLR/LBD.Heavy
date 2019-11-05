using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Models
{
    public class RoleAddModel
    {
        [Required]
        [Display(Name ="角色名字")]
        public string RoleName { get; set; }
    }
}
