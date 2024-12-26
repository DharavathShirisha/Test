using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Mvc;
using MVCCoreCF.Models;
using MVCCoreCF.Data;

namespace MVCCoreCF.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ApplicationContext context;

        public CategoryController(ILogger<CategoryController> logger1, ApplicationContext context1)
        {
            logger = logger1;
            context = context1;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(int CategoryId, string categoryName)
        {

            List<Category> list = new List<Category>();
            list = context.categories.ToList();

            foreach (var v1 in list)
            {
                if (v1.CategoryName.Equals(categoryName) || v1.InputCategoryId == CategoryId)
                {
                    return View("NotValid");
                }
            }
            Category c1 = new Category();
            c1.InputCategoryId = CategoryId;
            c1.CategoryName = categoryName;
            context.categories.Add(c1);
            context.SaveChanges();

            return RedirectToAction("FirstPage", "Home");


        }

        public IActionResult NotValid()
        {

            return View();
        }
        [HttpGet]
        public IActionResult ShowOnlyCategory()
        {
            var data = context.categories.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult DeleteCategory()
        {
            var data = context.categories.ToList();
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            Category p1 = context.categories.Find(id);
            List<Products> list = new List<Products>();
            list = context.products.ToList();

            foreach (var v1 in list)
            {
                if (p1.InputCategoryId == v1.Match)
                {
                    context.products.Remove(v1);
                    context.SaveChanges();
                }
            }
            if (p1 != null)
            {
                context.categories.Remove(p1);
                context.SaveChanges();
            }
            return RedirectToAction("FirstPage", "Home");
        }

        [HttpGet]
        public IActionResult UpdateCategory()
        {
            var data = context.categories.ToList();
            return View(data);

        }

        [HttpGet]
        public IActionResult CrudeOption()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = context.categories.Find(id);

            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateP(Category p1)
        {
            var data = context.categories.Find(p1.CategoryId);
            if (data != null)
            {
                var temp = data;
                data.InputCategoryId = p1.InputCategoryId;
                data.CategoryName = p1.CategoryName;
                context.categories.Update(data);
                context.SaveChanges();

                var product = context.products.ToList();

                foreach (var item in product)
                {
                    if (item.Match == temp.InputCategoryId)
                    {

                        item.Match = data.InputCategoryId;

                        var temp2 = context.products.Find(item.ProductId);
                        temp2.Match = item.IPNumber;

                        context.products.Update(temp2);
                        context.SaveChanges();

                    }
                }

                return RedirectToAction("FirstPage", "Home");
            }

            return RedirectToAction("NotValid", "Category");


        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
