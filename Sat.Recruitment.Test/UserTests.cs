using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.ValidationServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTests
    {
        private readonly User MockUser = new User
        {
            Name = "Mike",
            Email = "mike@gmail.com",
            Address = "Av. Juan G",
            Phone = "+349 1122354215",
            UserType = "Normal",
            Money = 124
        };

        [Fact]
        public void CreateUser_ReturnsOk()
        {
            var userService = new Mock<IUserService>();
            var usersValidateErrors = new Mock<IUserValidateErrors> { DefaultValue = DefaultValue.Mock };

            usersValidateErrors.Setup(x => x.ValidateErrors(MockUser)).ReturnsAsync(new List<string>());
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var userController = new UsersController(usersValidateErrors.Object);

            var result = userController.CreateUser(MockUser).Result;
            
            Assert.IsType<CreatedResult>(result);
            Assert.True(((Result)((ObjectResult)result).Value).IsSuccess);
        }

        [Fact]
        public void CreateUser_ReturnsDuplicatedUser()
        {            
            var userService = new Mock<IUserService>();
            var usersValidateErrors = new Mock<IUserValidateErrors> { DefaultValue  = DefaultValue.Mock };

            usersValidateErrors.Setup(x => x.ValidateErrors(MockUser)).ReturnsAsync(new List<string> { "The user is duplicated" });
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(true);

            var userController = new UsersController(usersValidateErrors.Object);

            var result = userController.CreateUser(MockUser).Result;

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(((Result)((ObjectResult)result).Value).Errors.Any());
            Assert.True(((Result)((ObjectResult)result).Value).Errors.Contains("The user is duplicated"));
        }

        [Fact]
        public void CreateUser_ReturnsValidationErrors()
        {
            var userService = new Mock<IUserService>();
            var usersValidateErrors = new Mock<IUserValidateErrors> { DefaultValue = DefaultValue.Mock };

            usersValidateErrors.Setup(x => x.ValidateErrors(MockUser)).ReturnsAsync(new List<string> { "The name is required" });
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(true);

            var userController = new UsersController(usersValidateErrors.Object);

            var result = userController.CreateUser(MockUser).Result;

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(((Result)((ObjectResult)result).Value).Errors.Any());
            Assert.True(((Result)((ObjectResult)result).Value).Errors.Contains("The name is required"));
        }

    }
}
