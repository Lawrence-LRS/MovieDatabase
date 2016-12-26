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
    public class HomeController : Controller
    {
        private MoviesDBEntities db = new MoviesDBEntities();

     
        // GET: Home
        public ActionResult Index(string searchString)
        {
            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(movies);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {

            var movieToView = (from m in db.Movies
                               where m.MovieID == id
                               select m).First();

            ViewBag.MovieID = id.Value;

            var ratings = db.Reviews.Where(d => d.MovieID.Equals(id.Value)).ToList();
            if(ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.rating);
                ViewBag.RatingSum = ratingSum;

                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(movieToView);
        }

        // GET: Home/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="ID")] Movie movieToCreate)
        {
            
            if (ModelState.IsValid)
            {
                db.Movies.Add(movieToCreate);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(movieToCreate);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var movieToEdit = (from m in db.Movies
                               where m.MovieID == id
                               select m).First();
            return View(movieToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Movie movieToEdit)
        {

            if (ModelState.IsValid)
            {
                db.Entry(movieToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieToEdit);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var movieToDelete = (from m in db.Movies
                                 where m.MovieID == id
                                 select m).First();
            return View(movieToDelete);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fcNotUsed)
        {
            Movie movieToDelete = db.Movies.Find(id);

            if (!ModelState.IsValid)
                return View(movieToDelete);

            db.Movies.Remove(movieToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}        