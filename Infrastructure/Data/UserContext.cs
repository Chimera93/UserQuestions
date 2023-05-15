using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public DbSet<UserQuestion> userQuestions { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

    }
}
