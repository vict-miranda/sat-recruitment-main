using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.ValidationServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.ValidationServices
{
    public class UserValidateErrors : IUserValidateErrors
    {
        private IUserService _userService;

        public UserValidateErrors(IUserService userService)
        {
            _userService = userService;
        }

        /// <inheritdoc/>
        public async Task<List<string>> ValidateErrors(User user)
        {
            var errors = new List<string>();
            var isDuplicated = await _userService.ValidateDuplicity(user);

            if (isDuplicated)
                errors.Add("The user is duplicated");
            if (string.IsNullOrEmpty(user.Name))
                //Validate if Name is null
                errors.Add("The name is required");
            if (string.IsNullOrEmpty(user.Email))
                //Validate if Email is null
                errors.Add("The email is required");
            if (string.IsNullOrEmpty(user.Address))
                //Validate if Address is null
                errors.Add("The address is required");
            if (string.IsNullOrEmpty(user.Phone))
                //Validate if Phone is null
                errors.Add("The phone is required");

            return errors;
        }
    }
}
