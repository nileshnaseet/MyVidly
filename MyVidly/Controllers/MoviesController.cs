using MyVidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Baahubali!" };

            return View(movie);
        }

        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseYear(int year, int month)
        { 
            return Content(String.Format("Year={0} & Month = {1}",year,month));
        }
    }
}