using Application.Interfaces;
using Application.Question;
using Application.Question.Queries.GetAllQuestions;
using Application.User;
using Infrastructure;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .ConfigureAppConfiguration((config) =>
            {
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();
                config.Build();
            })
            .ConfigureLogging((context, logging) => {
                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection("Logging");              
                logging.AddConfiguration(config);
                logging.AddConsole();              
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            })
            .ConfigureServices((hostContext, services) =>
            {
                
                services.AddApplicationServices(hostContext.Configuration);
                services.AddInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment.EnvironmentName);                

                services.AddHostedService<ConsoleHostedService>();                
               
            })
            .RunConsoleAsync();
    }
}

internal sealed class ConsoleHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IUserService _userService;
    private readonly IQuestionService _questionService;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IUserService userService,
        IQuestionService questionService)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _userService = userService;
        _questionService = questionService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {        
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    await Utilities.RunApplication(_userService, _questionService);
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception!");
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public static class Utilities 
{ 
    public static async Task RunApplication(IUserService _userService, IQuestionService _questionService)
    {
        Console.WriteLine("");
        Console.WriteLine("Hi, what is your name?");
        var nameEntered = Console.ReadLine();


        var existingUser = await _userService.GetUserByName(nameEntered);

        if (existingUser == null)
        {
            //Store flow
            Console.WriteLine("New user found!");
            Console.WriteLine("Would you like to store answers for security questions? (yes/no)");

            var input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input) && (input.ToLower() == "yes" || input.ToLower() == "y"))
            {
                var newUserID = await _userService.AddUser(nameEntered);

                var allQuestions = await _questionService.GetAllQuestions();

                Console.WriteLine("Please store answers to three questions.");
                List<int> answeredQuestionIDs = new();

                while (answeredQuestionIDs.Count < 3)
                {
                    answeredQuestionIDs = await Utilities.StoreQuestionAnswersForUsers(newUserID, allQuestions, answeredQuestionIDs, _userService, _questionService);
                }

                await RunApplication(_userService, _questionService);
                return;
            }
            else
            {
                await RunApplication(_userService, _questionService);
                return;
            }

        }
        else
        {
            //Answer flow
            var allQuestions = await _questionService.GetAllQuestions();

            List<int> answeredQuestionIDs = new();

            var userQuestions = await _userService.GetUserQuestions(existingUser.ID);

            Console.WriteLine("Please answer all of your questions.");

            foreach(var question in userQuestions)
            {
                var questionText = allQuestions.Where(q => q.Id == question.questionID).SingleOrDefault().Text;
                Console.WriteLine(questionText);
                var answer = Console.ReadLine();

                if (answer == question.answer)
                {
                    Console.WriteLine("Thank you for answering all of your questions!");
                    await RunApplication(_userService, _questionService);
                    return;
                }
            }

            Console.WriteLine("You did not answer your questions correctly.");
            await RunApplication(_userService, _questionService);
            return;
        }
    }

    public static async Task<List<int>> StoreQuestionAnswersForUsers(int userId, List<GetAllQuestionsDTO> questions, List<int> answeredQuestionIDS, IUserService _userService, IQuestionService _questionService)
    {
        List<int> answeredQuestionIDs = answeredQuestionIDS;
        foreach (var question in questions)
        {
            if(answeredQuestionIDs.Count < 3)
            {
                if (answeredQuestionIDs.Contains(question.Id) == false)
                {
                    Console.WriteLine(question.Text);
                    var questionAnswer = Console.ReadLine();

                    if (!String.IsNullOrWhiteSpace(questionAnswer))
                    {
                        var answeredSuccessfully = await _userService.AnswerQuestionForUser(userId, question.Id, questionAnswer);

                        if (answeredSuccessfully)
                        {
                            answeredQuestionIDs.Add(question.Id);
                        }                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Thank you for storing your question answers");
                await RunApplication(_userService, _questionService);
                return answeredQuestionIDs;
            }
        }

        return answeredQuestionIDs;
    }
}
