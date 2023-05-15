using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.User;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Domain.User.User> GetUserByName(string username);

        Task<int> AddUser(string username);


        Task<bool> AnswerQuestionForUser(int userID, int questionID, string answer);

        Task<List<UserQuestion>> GetUserQuestions(int userID);
    }
}
