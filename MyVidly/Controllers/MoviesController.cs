﻿using MyVidly.Models;
using MyVidly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyVidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }
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
            //var movie = _context.Movie.Include(c => c.Genre).ToList();
            
            return View();
        }

        public ActionResult Edit(Movie movie)
        {
            var movieInDB = _context.Movie.Include(m => m.Genre).SingleOrDefault(c => c.Id == movie.Id);

            var viewModel = new MovieFormViewModel(movieInDB)
            {
                Genres = _context.Genre
            };

            return View("MovieForm",viewModel);
        }

        public ActionResult New(Movie movie)
        {
            var movieInDB = _context.Movie.SingleOrDefault(c=>c.Id == movie.Id);

            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genre
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movie.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movie.Include(m => m.Genre).SingleOrDefault(c => c.Id == movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Movies");// View(movie);
        }

        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseYear(int year, int month)
        { 
            return Content(String.Format("Year={0} & Month = {1}",year,month));
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movie.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);
            
            return View(movie);
        }
    }
}