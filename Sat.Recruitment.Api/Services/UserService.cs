using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        /// <inheritdoc/>
        public async Task<bool> ValidateDuplicity(User newUser)
        {
            var _users = await ReadUsersFromFile();
            if (_users.Where(x => x.Email == newUser.Email || x.Phone == newUser.Phone).Count() > 0)
                return true;
            if (_users.Where(x => x.Name == newUser.Name && x.Address == newUser.Address).Count() > 0)
                return true;

            return false;
        }

        /// <inheritdoc/>
        public async Task<List<User>> ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            var users = new List<User>();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();

                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[5].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
