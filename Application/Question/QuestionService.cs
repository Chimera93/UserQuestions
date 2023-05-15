using Application.Interfaces;
using Application.Question.Queries.GetAllQuestions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IMediator _mediator;

        public QuestionService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<List<GetAllQuestionsDTO>> GetAllQuestions()
        {
            var result = await _mediator.Send(new GetAllQuestionsQuery());

            return result.questions;
        }
    }
}
