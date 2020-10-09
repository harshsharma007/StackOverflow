using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class AnswersModel
    {
        public int AnswerId { get; set; }
        [Required]
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
    }
}