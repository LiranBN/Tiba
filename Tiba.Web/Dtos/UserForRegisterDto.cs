using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tiba.Web.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Please enter password with at least 8 cherecters")]
        public string Password { get; set; }
    }
}
