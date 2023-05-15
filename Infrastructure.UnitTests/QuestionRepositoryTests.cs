using Domain.Question;
using Infrastructure.Data;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using FluentAssertions;
using NUnit.Framework;
using Domain.Repositories;

namespace Infrastructure.UnitTests
{
    public class QuestionRepositoryTests
    {
        private List<Question> populatedQuestionList { get; set; }

        private DbContextOptions<QuestionContext> contextOptions { get; set; }
        private QuestionContext questionContext { get; set; }

        private QuestionRepository questionRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            contextOptions = new DbContextOptionsBuilder<QuestionContext>()
            .UseInMemoryDatabase(databaseName: $"Data Test: {Guid.NewGuid()}")
            .Options;

            questionContext = new QuestionContext(contextOptions);

            questionRepository = new QuestionRepository(questionContext);

            populatedQuestionList = new List<Question>();

            populatedQuestionList.Add(new Question()
            {
                Id = 1,
                Text = "Question 1"
            });

            populatedQuestionList.Add(new Question()
            {
                Id = 2,
                Text = "Question 2"
            });

        }

        [Test]
        public async Task GetAllQuestions_PopulatedList_ShouldReturnResults()
        {
            questionContext.questions.AddRange(populatedQuestionList);
            questionContext.SaveChanges();

            var result = await questionRepository.GetAllQuestions();

            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAllQuestions_EmptyList_ShouldReturnEmptyList()
        {            
            var result = await questionRepository.GetAllQuestions();

            result.Should().BeEmpty();
        }
    }
}
