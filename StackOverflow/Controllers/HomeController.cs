using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Categories()
        {
            List<Category> objList = new List<Category>();
            using (var db = new NewRegEntities())
            {
                var query = from category in db.Categories
                            select new
                            {
                                CategoryId = category.CategoryId,
                                CategoryName = category.CategoryName
                            };

                foreach (var item in query)
                {
                    Category obj = new Category
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName
                    };
                    objList.Add(obj);
                }
            }
            return View(objList);
        }
    }
}