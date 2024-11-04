using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationBE.Models.ViewModel;
using WebApplicationBE.Models;
using PagedList;

namespace WebApplicationBE.Controllers
{
    public class HomeController1 : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        
            // GET: Views/Home
            public ActionResult Index(string searchTerm, int? page)
            {
                var model = new HomeProductVM();
                var products = db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    model.SearchTerm = searchTerm;
                    products = products.Where(p => p.ProductName.Contains(searchTerm) ||
                                                   p.ProductDecription.Contains(searchTerm) ||
                                                   p.Category.CategoryName.Contains(searchTerm));
                }
                int pageNumber = page ?? 1;
                int pageSize = 6;
                model.FeaturedProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(10).ToList();
                model.NewProducts = products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);
                return View(model);
            }
        

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}