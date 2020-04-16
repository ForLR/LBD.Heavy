using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Heavy.Application.ViewModels.Users
{
    public class ClaimTypeViewModel
    {
        [Required]
        public string Name { get; set; }
       
    }
}
