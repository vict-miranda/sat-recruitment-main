using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Validates if the user is duplicated
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>Return true if the user is duplicated; otherwise returns false</returns>
        Task<bool> ValidateDuplicity(User newUser);

        /// <summary>
        /// Read all the users from txt file
        /// </summary>
        /// <returns>A list of <see cref="User"/></returns>
        Task<List<User>> ReadUsersFromFile();
    }
}
