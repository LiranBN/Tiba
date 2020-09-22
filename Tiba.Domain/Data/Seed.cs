using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiba.Domain.Models;

namespace Tiba.Domain.Data
{
    public class Seed
    {
        private readonly DataContext context;
        public Seed(DataContext context)
        {
            this.context = context;

        }

        public void SeedData()
        {
            SeedItems<Repository>("../Tiba.Domain/Data/RepositorySeedData.json");
            SeedItems<User>("../Tiba.Domain/Data/UserSeedData.json",(User user)=>
            {
                byte[] passwordHash, passwordSalt;
                AuthRepository.CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PassworedSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();
            });
        }

        private void SeedItems<T>(string fileName,Action<T> action = null) where T : class
        {
            if (context.Set<T>().Any())
            {
                return;
            }

            var itemsData = System.IO.File.ReadAllText(fileName);
            var items = JsonConvert.DeserializeObject<List<T>>(itemsData);

            context.Set<T>().AddRange(items);
            foreach (var item in items)
            {
                action?.Invoke(item);

                context.Set<T>().Add(item);
            }
            context.SaveChanges();
        }

    }
}
