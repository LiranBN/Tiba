using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiba.Web.Dtos
{
    public class SearchRepoDto
    {
       
        [Required]
        public string Term { get; set; }
    }
}
