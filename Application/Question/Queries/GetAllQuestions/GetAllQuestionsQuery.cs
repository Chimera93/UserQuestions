using Application.User.Queries.GetUserByName;
using Domain.Question;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Question.Queries.GetAllQuestions
{
    public record GetAllQuestionsQuery : IRequest<Response>;

    public class Handler : IRequestHandler<GetAllQuestionsQuery, Response>
    {
        private readonly IQuestionRepository _questionRepository;

        public Handler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Response> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Question.Question> results = await _questionRepository.GetAllQuestions();
            List<GetAllQuestionsDTO> dtos = new();

            foreach(Domain.Question.Question q in results)
            {
                dtos.Add(new GetAllQuestionsDTO()
                {
                    Id = q.Id,
                    Text = q.Text
                });
            }

            return new Response(dtos);
        }
    }

    public record Response(List<GetAllQuestionsDTO> questions);
}
