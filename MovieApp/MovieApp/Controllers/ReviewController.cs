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
   
    }
}
