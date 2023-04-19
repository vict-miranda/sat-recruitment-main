using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Validates if the user is duplicated
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>Return true if the user is duplicated; otherwise returns false</returns>
        bool ValidateDuplicity(User newUser);

        /// <summary>
        /// Convert readed users from file to User objects
        /// </summary>
        /// <returns>A list of <see cref="User"/></returns>
        List<User> ReadUsers();
    }
}
