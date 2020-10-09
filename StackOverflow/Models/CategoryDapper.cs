using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class CategoryDapper
    {
        public IEnumerable<Category> GetList()
        {
            List<Category> objList = new List<Category>();

            using (var db = new NewRegEntities())
            {
                var query = from category in db.Categories select category;
                foreach (var a in query)
                {
                    Category obj = new Category
                    {
                        CategoryId = a.CategoryId,
                        CategoryName = a.CategoryName
                    };
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}