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
        private UserServices services;
        private readonly IUserRepositories _userRepositories;

        //public UnitTest2(UserServices services, IUserRepositories userRepositories)
        //{
        //    this.services = services;
        //    this._userRepositories = userRepositories;
        //}
        [TestMethod()]
        public async Task UserGetListTest()
        {
            #region arramge
            services = new UserServices(_userRepositories);

            var mockUserRepository = new Mock<IUserRepositories>();
            mockUserRepository.Setup(repo => repo.GetAsync())
                              .ReturnsAsync(new List<PiUser>
                              {
                                  new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                                  new PiUser { Id=2, GivenName = "manb", Email = "manb@gmail.com" }
                              });

            var userService = new UserServices(mockUserRepository.Object);
            #endregion

            #region actual

            IEnumerable<PiUser> result = await userService.GetUsers();
            #endregion

            #region assert

            Console.WriteLine($"Expected: {result}, Actual: {result}");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            #endregion
        }
    }
}