using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCCoreCF.Data;
using MVCCoreCF.Models;

namespace MVCCoreCF.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly ApplicationContext context;

        public ProductController(ILogger<ProductController> logger1, ApplicationContext context1)
        {
            logger = logger1;
            context = context1;
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            var data = context.categories.ToList();
            if (data == null)
                return RedirectToAction("NotValid");
            return View(data);
        }
        [HttpPost]
        public IActionResult AddProduct(int IPnumber, string ProductName, int Match)
        {
            List<Products> list = new List<Products>();
            list = context.products.ToList();

            foreach (var v1 in list)
            {
                if (v1.ProductName.Equals(ProductName) || v1.IPNumber == IPnumber)
                {
                    return RedirectToAction("NotValid", "Category");
                }
            }
            Products p2 = new Products();
            p2.IPNumber = IPnumber;
            p2.ProductName = ProductName;
            p2.Match = Match;
            context.products.Add(p2);
            context.SaveChanges();
            return RedirectToAction("FirstPage", "Home");
        }

        [HttpGet]
        public IActionResult ShowOnlyProduct()
        {
            var data = context.products.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult DeleteProduct()
        {
            var data = context.products.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = context.products.Find(id);
            context.products.Remove(data);
            context.SaveChanges();
            return RedirectToAction("FirstPage", "Home");
        }

        [HttpGet]
        public IActionResult ShowAllProductWithCategory()
        {
            var products = context.products.ToList();
            var categories = context.categories.ToList();

            foreach (var product in products)
            {
                product.Category = categories
                    .Where(c => c.InputCategoryId == product.Match)
                    .ToList();
            }

            return View(products);
        }

        [HttpGet]
        public IActionResult UpdateProduct()
        {
            var data = context.products.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = context.products.Find(id);

            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateP(Products p1)
        {
            var data = context.products.Find(p1.ProductId);
            data.IPNumber = p1.IPNumber;
            data.ProductName = p1.ProductName;
            context.products.Update(data);
            context.SaveChanges();
            return RedirectToAction("FirstPage", "Home");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
