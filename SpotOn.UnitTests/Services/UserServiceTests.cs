using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotOn.ApplicationLogic.Entities.Tags;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.ApplicationLogic.Mappings.AutoMapper;
using SpotOn.ApplicationLogic.Services;
using SpotOn.ApplicationLogic.ViewModels.Users;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpotOn.UnitTests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IBaseRepository<User>> _userRepositoryMock;
        private IMapper _mapper;

        private Guid _userId = Guid.NewGuid();
        private string _userName = "John Doe";
        private string _email = "john@Gmail.com";
        private string _password = "John$sign";
        DateTimeOffset _createdAt = DateTimeOffset.Now;

        private Guid _userId1 = Guid.NewGuid();
        private string _userName1 = "Mary Doe";
        private string _email1 = "mary@Gmail.com";
        private string _password1 = "mary*sign";
        DateTimeOffset _createdAt1 = DateTimeOffset.Now;

        private RegisterUserEntity _user;
        private LogInEntity _userLogin;

        [TestInitialize]
        public void SetUp()
        {
            _user = new RegisterUserEntity
            {
                Email = _email,
                UserName = _userName,
                Password = _password
            };

            _userLogin = new LogInEntity
            {
                Email = _email,
                Password = _password
            };

            var mockUserList = new List<User>
            {
                new User
                {
                    Id = _userId,
                    Email = _email,
                    Username = _userName,
                    Password = _password,
                    CreatedAt = _createdAt
                    
                },
                new User
                {
                    Id = _userId1,
                    Email = _email1,
                    Username = _userName1,
                    Password = _password1,
                    CreatedAt = _createdAt1
                }
            };


            _userRepositoryMock = new Mock<IBaseRepository<User>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToViewModelProfile());
            });
            _mapper = config.CreateMapper();

            _userRepositoryMock.Setup(repo => repo.All())
                .Returns(mockUserList);

            _userRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((Expression<Func<User, bool>> expression) => mockUserList.AsQueryable().Where(expression));

            _userService = new UserService(_userRepositoryMock.Object,
                _mapper);

        }

        [TestMethod]
        public async Task RegisterAsync_RegistersUser_ReturnsNewUserDetails()
        {
            var result = await _userService.RegisterAsync(_user);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Email, _email);
            Assert.AreEqual(result.Password, _password);
        }

        [TestMethod]
        public void LogInAsync_UserNotFoud_ReturnsNull()
        {
            _userLogin.Password = "12345";

            var result = _userService.LogInAsync(_userLogin);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LogInAsync_UserFoud_ReturnsLoggedInUserDetails()
        {
            var result = _userService.LogInAsync(_userLogin);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Password, _password);
            Assert.AreEqual(result.Email, _email);
        }
    }
}
