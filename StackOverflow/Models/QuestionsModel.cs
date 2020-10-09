using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackOverflow.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace StackOverflow.Models
{
    public class QuestionsModel
    {
        public int? QuestionId { get; set; }
        [Required]
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public string Answer { get; set; }
        [NotMapped]
        public SelectList CategoryList { get; set; }
    }
}