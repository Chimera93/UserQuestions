using Domain.User;
using Infrastructure.Data;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using FluentAssertions;
using NUnit.Framework;

namespace Infrastructure.UnitTests
{
    public class UserRepositoryTests
    {
        private List<User> populatedUserList { get; set; }

        private DbContextOptions<UserContext> contextOptions { get; set; }
        private UserContext userContext { get; set;}

        private UserRepository userRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            contextOptions = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: $"Data Test: {Guid.NewGuid()}")
            .Options;

            userContext = new UserContext(contextOptions);

            userRepository = new UserRepository(userContext);

            populatedUserList = new List<User>();

            populatedUserList.Add(new User() 
            {  
                Id = 1,
                Name = "User 1"
            });

            populatedUserList.Add(new User()
            {
                Id = 2,
                Name = "User 2"
            });

        }

        [Test]
        public async Task GetUserByName_ExistingUser()
        {
            userContext.users.AddRange(populatedUserList);
            userContext.SaveChanges();

            var result = await userRepository.GetUserByName("User 1");

            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetUserByName_NonExistingUser()
        {
            userContext.users.AddRange(populatedUserList);
            userContext.SaveChanges();

            var result = await userRepository.GetUserByName("Non existant user");

            result.Should().BeNull();
        }

        [Test]
        public async Task AddUser_ReturnsIdentity()
        {
            var result = await userRepository.AddUser("Brand new user");

            result.Equals(1);
        }
    }
}