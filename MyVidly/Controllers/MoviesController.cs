using MyVidly.Models;
using MyVidly.ViewModels;
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
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
        public ActionResult Index()
        {
            var movie = new List<Movie>();

            movie.Add(new Movie { Name = "Baahubali Part 1" });
            movie.Add(new Movie { Name = "Baahubali Part 2" });

            return View(movie);
        }

        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseYear(int year, int month)
        { 
            return Content(String.Format("Year={0} & Month = {1}",year,month));
        }
    }
}