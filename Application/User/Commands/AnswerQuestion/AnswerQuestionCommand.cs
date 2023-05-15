using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.AnswerQuestion
{
    public record AnswerQuestionCommand : IRequest<Response>
    {
        public int userID { get; set; }
        public int questionID { get; set; }
        public string answer { get; set; }
    }

    public class Handler : IRequestHandler<AnswerQuestionCommand, Response>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(AnswerQuestionCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.AnswerQuestionForUser(command.userID, command.questionID, command.answer);

            return new Response(result);
        }
    }

    public record Response(bool success);
}
