using Application.User.Queries.GetUserByName;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.AddUser
{

    public record AddUserCommand : IRequest<Response>
    {
        public string Name { get; set;}
    }

    public class Handler : IRequestHandler<AddUserCommand, Response>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.AddUser(command.Name);

            return new Response(result);
        }
    }

    public record Response(int ID);
}
