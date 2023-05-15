using Application.User.Queries.GetUserByName;
using Application.User.Queries.GetUserQuestions;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<GetUserByNameDTO> GetUserByName(string username);

        Task<int> AddUser(string username);

        Task<bool> AnswerQuestionForUser(int userID, int questionID, string answer);

        Task<List<GetUserQuestionsDTO>> GetUserQuestions(int userID);
    }
}
