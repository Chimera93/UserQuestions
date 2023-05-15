using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserQuestions
{
    public class GetUserQuestionsDTO
    {
        public int userID { get; set; }

        public int questionID { get; set; }

        public string answer { get; set; }
    }
}
