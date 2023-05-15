using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data;
using System.Reflection;
using Domain.Repositories;
using Infrastructure.SQLServer;
using Application;
using Application.Interfaces;
using Application.Question;
using Application.User;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatrEntrypoint).Assembly));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionService, QuestionService>();

        services.AddLogging();

        return services;
    }
}