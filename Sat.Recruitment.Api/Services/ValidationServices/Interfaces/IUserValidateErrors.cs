using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.ValidationServices.Interfaces
{
    public interface IUserValidateErrors
    {
        /// <summary>
        /// Validates if the user information is correct
        /// </summary>
        /// <param name="user">User information to validate</param>
        /// <returns>If the validation contains errors, return a list with the errors; otherwise return an empty list</returns>
        public List<string> ValidateErrors(User user);
    }
}
