using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class NewQuestion
    {
        public QuestionsModel QuestionDetails { get; set; }
        public AnswersModel AnswerDetails { get; set; }
        public List<AnswersModel> ListAnswerDetails { get; set; }
    }
}