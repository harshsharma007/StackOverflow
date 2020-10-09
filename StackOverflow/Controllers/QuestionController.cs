using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question(int IsFiltered)
        {
            List<QuestionsModel> objList = new List<QuestionsModel>();
            using (var db = new NewRegEntities())
            {
                var query = from question in db.Questions
                            join category in db.Categories on question.CatergoryId equals category.CategoryId
                            select new QuestionsModel()
                            {
                                QuestionId = question.QuestionId,
                                Question = question.Question1,
                                Category = category.CategoryName,
                                CategoryId = category.CategoryId,
                                QuestionDateAndTime = question.QuestionDateAndTime
                            };

                foreach (var item in query)
                {
                    QuestionsModel obj = new QuestionsModel
                    {
                        QuestionId = item.QuestionId,
                        Question = item.Question,
                        CategoryId = item.CategoryId,
                        Category = item.Category,
                        QuestionDateAndTime = item.QuestionDateAndTime
                    };
                    objList.Add(obj);
                }
            }
            return View(objList);
        }

        public ActionResult GetQuestionById(int QuestionId)
        {
            NewQuestion obj = new NewQuestion();
            List<AnswersModel> objlist = new List<AnswersModel>();
            using (var db = new NewRegEntities())
            {
                var query = from question in db.Questions
                            join category in db.Categories on question.CatergoryId equals category.CategoryId
                            where question.QuestionId == QuestionId
                            select new QuestionsModel()
                            {
                                QuestionId = question.QuestionId,
                                Question = question.Question1,
                                Category = category.CategoryName,
                                CategoryId = category.CategoryId,
                                QuestionDateAndTime = question.QuestionDateAndTime
                            };
                obj.QuestionDetails = query.FirstOrDefault();

                var answerQuery = from answer in db.Answers
                                  join question in db.Questions on answer.QuestionId equals question.QuestionId
                                  where question.QuestionId == QuestionId
                                  select new AnswersModel()
                                  {
                                      AnswerId = answer.AnswerId,
                                      Answer = answer.Answer1,
                                      QuestionId = (int)answer.QuestionId,
                                      AnswerDateAndTime = answer.AnswerDateAndTime
                                  };

                foreach (var item in answerQuery)
                {
                    AnswersModel objNew = new AnswersModel
                    {
                        AnswerId = item.AnswerId,
                        Answer = item.Answer,
                        QuestionId = item.QuestionId,
                        AnswerDateAndTime = item.AnswerDateAndTime
                    };
                    objlist.Add(objNew);
                }
                obj.ListAnswerDetails = objlist;

                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult GetQuestionById(AnswersModel answersModel)
        {
            if(ModelState.IsValid)
            {
                using (var context = new NewRegEntities())
                {
                    var answer = new StackOverflow.Models.Answer
                    {
                        Answer1 = answersModel.Answer,
                        QuestionId = answersModel.QuestionId,
                        AnswerDateAndTime = DateTime.Now
                    };
                    context.Answers.Add(answer);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("GetQuestionById", new { QuestionId = answersModel.QuestionId });
        }

        public ActionResult AskQuestion()
        {
            QuestionsModel objModel = new QuestionsModel();
            CategoryDapper objCategoryList = new CategoryDapper();
            objModel.CategoryList = new SelectList(objCategoryList.GetList(), "CategoryId", "CategoryName");
            ViewBag.CategoryList = objModel.CategoryList;
            return View();
        }

        [HttpPost]
        public ActionResult AskQuestion(QuestionsModel questions)
        {
            QuestionsModel objModel = new QuestionsModel();
            CategoryDapper objCategoryList = new CategoryDapper();
            objModel.CategoryList = new SelectList(objCategoryList.GetList(), "CategoryId", "CategoryName");
            ViewBag.CategoryList = objModel.CategoryList;

            if (ModelState.IsValid)
            {
                using (var context = new NewRegEntities())
                {
                    var question = new StackOverflow.Models.Question
                    {
                        Question1 = questions.Question,
                        CatergoryId = (int)questions.CategoryId,
                        QuestionDateAndTime = DateTime.Now
                    };
                    context.Questions.Add(question);
                    context.SaveChanges();
                }
                return RedirectToAction("Question", new { IsFiltered = 0 });
            }
            return View("AskQuestion");
        }
    }
}