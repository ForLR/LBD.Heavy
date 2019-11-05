using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Models
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

       

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Rememberme { get; set; }
    }
}
