using Domain.Question;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Data
{
    public class QuestionContext : DbContext
    {
        public DbSet<Question> questions { get; set; }

        public QuestionContext(DbContextOptions<QuestionContext> options) : base(options)
        {
           
        }
    }
}
