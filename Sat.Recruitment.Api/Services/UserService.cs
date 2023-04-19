using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        /// <inheritdoc/>
        public bool ValidateDuplicity(User newUser)
        {
            var _users = ReadUsers();
            if (_users.Where(x => x.Email == newUser.Email || x.Phone == newUser.Phone).Count() > 0)
                return true;
            if (_users.Where(x => x.Name == newUser.Name && x.Address == newUser.Address).Count() > 0)
                return true;

            return false;
        }

        /// <inheritdoc/>
        public List<User> ReadUsers()
        {
            var reader = ReadUsersFromFile();
            var users = new List<User>();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;

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
            reader.Close();

            return users;
        }

        /// <summary>
        /// Read all the users from txt file
        /// </summary>
        /// <returns>An instance of <see cref="StreamReader"/></returns>
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
