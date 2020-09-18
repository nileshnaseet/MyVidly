using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyVidly.Models;
using MyVidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace MyVidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        // GET api/<controller>
        public IHttpActionResult GetMovies()
        {
            //var movie = _context.Movie.Include(c => c.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);

            var movieDto = _context.Movie
                .Include(c => c.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDto);
        }

        // GET api/<controller>/5
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movie.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id),movieDto);

        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _context.Movie.SingleOrDefault(a => a.Id == id);

            if (movieInDB == null)
                return NotFound();

            movieInDB = Mapper.Map(movieDto, movieInDB);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var movieInDB = _context.Movie.SingleOrDefault(a => a.Id == id);

            if (movieInDB == null)
                return NotFound();

            _context.Movie.Remove(movieInDB);

            _context.SaveChanges();

            return Ok();
        }
    }
}