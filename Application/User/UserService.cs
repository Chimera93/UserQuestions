using Application.Interfaces;
using Application.User.Commands.AddUser;
using Application.User.Commands.AnswerQuestion;
using Application.User.Queries.GetUserByName;
using Application.User.Queries.GetUserQuestions;
using Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetUserByNameDTO> GetUserByName(string username)
        {
            var result = await _mediator.Send(new GetUserByNameQuery() 
            { 
                Name = username 
            });

            return result?.user;
        }

        public async Task<int> AddUser(string username)
        {
            var result = await _mediator.Send(new AddUserCommand()
            {
                Name = username
            });

            return result.ID;
        }

        public async Task<bool> AnswerQuestionForUser(int userID, int questionID, string answer)
        {
            var result = await _mediator.Send(new AnswerQuestionCommand()
            {
                userID = userID,
                questionID = questionID,
                answer = answer
            });

            return result.success;
        }

        public async Task<List<GetUserQuestionsDTO>> GetUserQuestions(int userID)
        {
            var result = await _mediator.Send(new GetUserQuestionsQuery() 
            { 
                userID = userID
            });

            return result.dtos;
        }
    }
}
