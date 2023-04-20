using Moq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.ValidationServices;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Services.ValidationServices
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserValidateErrorsTests
    {
        private User MockUser = new User
        {
            Name = "Mike",
            Email = "mike@gmail.com",
            Address = "Av. Juan G",
            Phone = "+349 1122354215",
            UserType = "Normal",
            Money = 124
        };

        [Fact]
        public async Task ValidateUser_ReturnsOk()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var usersValidateErrors = new UserValidateErrors(userService.Object);

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task ValidateUser_ReturnsDuplicityError()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(true);

            var usersValidateErrors = new UserValidateErrors(userService.Object);
            MockUser.Name = string.Empty;

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Contains("The user is duplicated"));
        }

        [Fact]
        public async Task ValidateUser_ReturnsNameError()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var usersValidateErrors = new UserValidateErrors(userService.Object);
            MockUser.Name = string.Empty;

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Contains("The name is required"));
        }

        [Fact]
        public async Task ValidateUser_ReturnsEmailError()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var usersValidateErrors = new UserValidateErrors(userService.Object);
            MockUser.Email = string.Empty;

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Contains("The email is required"));
        }

        [Fact]
        public async Task ValidateUser_ReturnsAddressError()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var usersValidateErrors = new UserValidateErrors(userService.Object);
            MockUser.Address = string.Empty;

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Contains("The address is required"));
        }

        [Fact]
        public async Task ValidateUser_ReturnsPhoneError()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);

            var usersValidateErrors = new UserValidateErrors(userService.Object);
            MockUser.Phone = string.Empty;

            var result = await usersValidateErrors.ValidateErrors(MockUser);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Contains("The phone is required"));
        }
    }
}
