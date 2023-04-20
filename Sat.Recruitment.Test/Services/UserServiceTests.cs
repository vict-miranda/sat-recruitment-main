using Moq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserServiceTests
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
        public async Task ValidateUserDuplicity_ReturnsFalse()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(false);
            userService.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(new List<User>());

            var result = await userService.Object.ValidateDuplicity(MockUser);

            Assert.False(result);
        }

        [Fact]
        public async Task ValidateUserDuplicity_ReturnsTrue()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ValidateDuplicity(MockUser)).ReturnsAsync(true);
            userService.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(new List<User>());

            var result = await userService.Object.ValidateDuplicity(MockUser);

            Assert.True(result);
        }

        [Fact]
        public async Task ReadUsersFromFile_ReturnsOk()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(new List<User> { MockUser });

            var result = await userService.Object.ReadUsersFromFile();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.True(result.Where(x => x.Name == "Mike").Count() > 0);
        }
    }
}
