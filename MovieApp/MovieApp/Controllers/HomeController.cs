using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var movieToView = (from m in db.Movies
                               where m.MovieID == id
                               select m).First();
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
        public ActionResult Create([Bind(Exclude = "Id")] Movie movieToCreate)
        { 

            if (ModelState.IsValid)
            {
                db.Movies.Add(movieToCreate);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
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