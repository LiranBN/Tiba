using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiba.Domain.Models;

namespace Tiba.Domain.Data
{
    public interface IRepositoryRepo
    {
        Task<IEnumerable<Repository>> GetRepositoriesAsync();
        Task<IEnumerable<Repository>> SearchRepositoryAsync(string term,string name);

    }
}
