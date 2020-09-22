using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiba.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PassworedSalt { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
