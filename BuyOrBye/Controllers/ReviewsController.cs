using BuyOrBye.Data;
using BuyOrBye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BuyOrBye.Controllers
{
    public class ReviewsController : Controller
    {
        //db connection
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reviews = _context.Review
               .Include(p => p.Product)
               .OrderBy(p => p.Username)
               .ToList();

            return View(reviews);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_context.Product.OrderBy(c => c.Name).ToList(), "ProductId", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind("Comment,Rating,ReviewDate,Username,ProductId")] Review review)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            _context.Review.Add(review);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        //Update
        [HttpPost]
        [Authorize]
        public IActionResult Edit([Bind("ReviewId,Comment,Rating,ReviewDate,Username,ProductId")] Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            _context.Review.Update(review);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var review = _context.Review.Find(id);

            if (review == null)
            {
                return NotFound();
            }

            ViewBag.ProductId = new SelectList(
                _context.Product.OrderBy(p => p.Name).ToList(),
                "ProductId",
                "Name");

            return View(review);
        }

        //Delete
        [Authorize]
        public IActionResult Delete(int id)
        {
            var review = _context.Review.Find(id);

            if (review == null)
            {
                return NotFound();
            }

            //delete from the database
            _context.Review.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
