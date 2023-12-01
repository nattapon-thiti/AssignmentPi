using AssignmentPiSecurities.Controllers.Users;
using Moq;
using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    [TestClass()]
    public class UnitTest2
    {
        private UserServices _userServices;
        private readonly IUserRepositories _userRepositories;
        private readonly Mock<IUserRepositories> _mockUserRepositories;

        public UnitTest2()
        {
            _mockUserRepositories = new Mock<IUserRepositories>();
            _userServices = new UserServices(_mockUserRepositories.Object);
        }
        [TestMethod()]
        public async Task UserGetListTest()
        {
            #region arramge
            _userServices = new UserServices(_userRepositories);

            var mockUserRepository = new Mock<IUserRepositories>();
            mockUserRepository.Setup(repo => repo.GetAsync(null))
                              .ReturnsAsync(new List<PiUser>
                              {
                                  new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                                  new PiUser { Id=2, GivenName = "manb", Email = "manb@gmail.com" }
                              });

            var userService = new UserServices(mockUserRepository.Object);
            #endregion

            #region actual

            IEnumerable<PiUser> result = await userService.GetUsers(null);
            #endregion

            #region assert

            Console.WriteLine($"Expected: {result}, Actual: {result}");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            #endregion
        }

        [TestMethod()]
        public async Task GetUsers_ExceptionHandling()
        {
            // Arrange
            _mockUserRepositories.Setup(repo => repo.GetAsync(It.IsAny<string>()))
                              .ThrowsAsync(new Exception("Simulated error"));

            var userService = new UserServices(_mockUserRepositories.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.GetUsers("errorRequest"));
        }
    }
}