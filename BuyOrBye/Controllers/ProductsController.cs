using BuyOrBye.Data;
using BuyOrBye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;


namespace BuyOrBye.Controllers
{
    public class ProductsController : Controller
    {
        //db connection
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //order by... ref: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-10.0&
        public IActionResult Index(string sortOrder)
        {
            var products = from p in _context.Product select p;

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;

                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return View(products.ToList());
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind("Name,Brand,Price")] Product product)
        {
            //validate input
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Product.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //Update
        [HttpPost]
        [Authorize]
        public IActionResult Edit([Bind("ProductId,Name,Brand,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Product.Update(product);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            //delete from the database
            _context.Product.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
