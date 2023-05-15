using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLServer
{

    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionContext _questionContext;

        public QuestionRepository(QuestionContext questionContext)
        {
            _questionContext = questionContext;
        }

        public async Task<List<Domain.Question.Question>> GetAllQuestions()
        {
            return await _questionContext.questions.ToListAsync();
        }
    }
}
