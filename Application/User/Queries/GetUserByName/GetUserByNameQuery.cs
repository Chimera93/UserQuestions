using AutoMapper;
using Azure;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.Queries.GetUserByName
{
    public record GetUserByNameQuery : IRequest<Response>
    {
        public string Name { get; set; }
    }

    public class Handler : IRequestHandler<GetUserByNameQuery, Response>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByName(request.Name);

            if(result == null)
            {
                return null;
            }
            else
            {
                return new Response(new GetUserByNameDTO()
                {
                    ID = result.Id,
                    Name = result.Name
                });
            }            
        }
    }

    public record Response(GetUserByNameDTO user);
}
