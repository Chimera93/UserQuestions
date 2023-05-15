using Application.Question.Queries.GetAllQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuestionService
    {
        Task<List<GetAllQuestionsDTO>> GetAllQuestions();
    }
}
