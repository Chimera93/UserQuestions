using Domain.Question;
using Domain.User;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TestDBInitializer
    {

        private readonly UserContext _userContext;
        private readonly QuestionContext _questionContext;

        public TestDBInitializer(UserContext userContext, QuestionContext questionContext)
        {
            _userContext = userContext;
            _questionContext = questionContext;
        }

        public void Init()
        {
            InitUserContext();
            InitQuestionContext();
        }

        public void InitUserContext()
        {
            _userContext.users.Add(new User()
            {
                Id = 1,
                Name = "TestUser1"
            });

            _userContext.users.Add(new User()
            {
                Id = 1,
                Name = "TestUser2"
            });

            _userContext.userQuestions.Add(new UserQuestion()
            {
                Id = 1,
                UserId = 1,
                QuestionId = 1,
                Answer = "answer1"
            });

            _userContext.userQuestions.Add(new UserQuestion()
            {
                Id = 2,
                UserId = 1,
                QuestionId = 2,
                Answer = "answer2"
            });

            _userContext.userQuestions.Add(new UserQuestion()
            {
                Id = 3,
                UserId = 1,
                QuestionId = 3,
                Answer = "answer3"
            });

            _userContext.SaveChanges();                    
        }

        public void InitQuestionContext()
        {
            _questionContext.questions.Add(new Question()
            {
                Id = 1,
                Text = "Question 1"
            });

            _questionContext.questions.Add(new Question()
            {
                Id = 2,
                Text = "Question 2"
            });

            _questionContext.questions.Add(new Question()
            {
                Id = 3,
                Text = "Question 3"
            });

            _questionContext.SaveChanges();
        }       
    }
}
