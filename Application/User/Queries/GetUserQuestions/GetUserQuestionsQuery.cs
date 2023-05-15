using Application.User.Queries.GetUserByName;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserQuestions
{

    public record GetUserQuestionsQuery : IRequest<Response>
    {
        public int userID { get; set; }
    }

    public class Handler : IRequestHandler<GetUserQuestionsQuery, Response>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(GetUserQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _userRepository.GetUserQuestions(request.userID);

            List<GetUserQuestionsDTO> dtos = new();

            foreach(var question in questions)
            {
                dtos.Add(new GetUserQuestionsDTO()
                {
                    userID = question.UserId,
                    questionID = question.QuestionId,
                    answer = question.Answer
                });
            }

            return new Response(dtos);
        }
    }

    public record Response(List<GetUserQuestionsDTO> dtos);
}
