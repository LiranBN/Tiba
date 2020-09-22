using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tiba.Domain.Models
{
  
    public class Repository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Name { get; set; }

        public List<Favorite> Favorites { get; set; } = new List<Favorite>();

        [NotMapped]
        public bool? AddToFavorite { get; set; }

    }
}
