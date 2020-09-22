using System;
using System.Collections.Generic;
using System.Text;

namespace Tiba.Domain.Models
{
    public class Favorite
    {
        public int RepositoryId { get; set; }
        public Repository Repository { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
