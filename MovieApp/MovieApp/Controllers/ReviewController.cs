using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MovieApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace MovieApp.Controllers
{
    public class ReviewController : Controller
    {
        private MoviesDBEntities db = new MoviesDBEntities();

        // GET: Review
        public ActionResult Index(Review reviews)
        {
            return View(reviews);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var MovieID = int.Parse(form["MovieID"]);
            var rating = int.Parse(form["Rating"]);

            Review movieReview = new Review()
            {
                MovieID = MovieID,
                Comment = comment,
                rating = rating,
                DateReviewed = DateTime.Now
            };

            db.Reviews.Add(movieReview);
            db.SaveChanges();

            return RedirectToAction("Details", "", new { id = MovieID });
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
