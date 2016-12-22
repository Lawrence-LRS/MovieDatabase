using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MovieApp.Models;
using System.Data.Entity;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDBEntities1 _db = new MoviesDBEntities1();

        // GET: Home
        public ActionResult Index()

        {

            return View(_db.Movies1.ToList());

        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            //return View();

            var movieToView = (from m in _db.Movies1

                               where m.Id == id

                               select m).First();

            return View(movieToView);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Movie movieToCreate)

        {

            if (!ModelState.IsValid)

                return View();
            _db.Movies1.Add(movieToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: /Home/Edit/5

        public ActionResult Edit(int id)

        {
            var movieToEdit = (from m in _db.Movies1
                               where m.Id == id
                               select m).First();
            return View(movieToEdit);

        }

        //

        // POST: /Home/Edit/5 

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Edit(Movie movieToEdit)

        {
            

            if (ModelState.IsValid)
            {
                _db.Entry(movieToEdit).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieToEdit);
          


            

         
            
              
           
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var movieToDelete = (from m in _db.Movies1

                               where m.Id == id

                               select m).First();

            return View(movieToDelete);
        }

        // POST: Home/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete( Movie movieToDelete )
        {
            if (!ModelState.IsValid)
                return View();

            _db.Entry(movieToDelete).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
