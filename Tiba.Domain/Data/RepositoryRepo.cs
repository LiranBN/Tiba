using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Tiba.Domain.Models;

namespace Tiba.Domain.Data
{
    public class RepositoryRepo : IRepositoryRepo
    {
        private readonly DataContext context;

        public RepositoryRepo(DataContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Repository>> GetRepositoriesAsync() => await context.Repositories.ToListAsync();

        public async Task<IEnumerable<Repository>> SearchRepositoryAsync(string term,string name)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == name);
            var sql = $"SELECT Id,Name ,case when F.UserId is null then 1 else 0 end AddToFavorite" +
                $"FROM Repositories R left outer join Favorites F on R.Id =F.RepositoryId and F.UserId = {user.Id}" +
                $"where name like '%{term}%'";
            return await context.Repositories.FromSqlRaw(sql).ToListAsync();
        }
    }
}