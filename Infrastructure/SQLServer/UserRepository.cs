using Domain.Repositories;
using Domain.User;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLServer
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<Domain.User.User> GetUserByName(string username)
        {
            return await _userContext.users.Where(u => u.Name == username).FirstOrDefaultAsync();
        }

        public async Task<int> AddUser(string username)
        {
            var user = new Domain.User.User()
            {
                Name = username
            };

            await _userContext.users.AddAsync(user);

            await _userContext.SaveChangesAsync();

            return user.Id;

        }

        public async Task<bool> AnswerQuestionForUser(int userID, int questionID, string answer)
        {
            try
            {
                await _userContext.userQuestions.AddAsync(new Domain.User.UserQuestion
                {
                    UserId = userID,
                    QuestionId = questionID,
                    Answer = answer
                });

                await _userContext.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<UserQuestion>> GetUserQuestions(int userID)
        {
            return await _userContext.userQuestions.Where(q => q.UserId == userID).ToListAsync();
        }
    }
}
