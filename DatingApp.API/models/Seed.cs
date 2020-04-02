using System.Collections.Generic;
using DatingApp.API.Controllers.models;
using Newtonsoft.Json;

namespace DatingApp.API.models
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            var userData = System.IO.File.ReadAllText("models/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var i in users)
            {
                byte[] passwordhash,passwordSalt;
                CreatePasswordHash("password",out passwordhash,out passwordSalt);
                i.PasswordHash = passwordhash;
                i.PasswordSalt = passwordSalt;
                i.Username = i.Username.ToLower();
                context.Users.Add(i);
            }
            context.SaveChanges();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
    
}