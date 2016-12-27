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
        //Initializes a database
        private MoviesDBEntities db = new MoviesDBEntities();


     
        // GET: Home
        public ActionResult Index(string searchString)
        {
            //Query to populate the index page
            var movies = from m in db.Movies
                         select m;

            //Updates index population based on searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(movies);
        }


        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {

            // selects specific movie to view
            var movieToView = (from m in db.Movies
                               where m.MovieID == id
                               select m).First();

            //Allows use of id value in view
            ViewBag.MovieID = id.Value;

            //Takes reviews if not 0 sums them to a total and total number to use in view.
            var ratings = db.Reviews.Where(d => d.MovieID.Equals(id.Value)).ToList();

            if(ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.rating);
                var ratingCount = ratings.Count();
                var ratingAvg = ratingSum / ratingCount;
                
                movieToView.RatingAVG = ratingAvg;
                db.SaveChanges();

            } else {
                movieToView.RatingAVG = 0;
                db.SaveChanges();

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
            //Adds the movie form to db
            if (ModelState.IsValid)
            {
                db.Movies.Add(movieToCreate);
                movieToCreate.RatingAVG = 0;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(movieToCreate);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            //Query for movie with ID
            var movieToEdit = (from m in db.Movies
                               where m.MovieID == id
                               select m).First();
            return View(movieToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Movie movieToEdit, FormCollection form)
        {

            if (ModelState.IsValid)
            {
                db.Entry(movieToEdit).State = EntityState.Modified;
                movieToEdit.RatingAVG = int.Parse(form["RatingAVG"]);
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

            if (ModelState.IsValid)
            {
                db.Movies.Remove(movieToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

                return View(movieToDelete);
        }
    }
}        