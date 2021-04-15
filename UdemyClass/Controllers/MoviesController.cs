using UdemyClass.Models;
using UdemyClass.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UdemyClass.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _Context;

        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie();
            movie.Name = "3 Idiots";

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1", Id =1},
                new Customer {Name = "Customer 2", Id= 2}
            };

            var ViewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers

            };

            return View(ViewModel);
        }
        public ActionResult Edit(int id)
        {
            return View();

        }
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            var ViewModel = new ListOfMovies
            {
                Movies = _Context.Movies.ToList()
            };
            return View(ViewModel);
        }
        [Route("movies/released/{Year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByreleaseDate(int year, int month)
        {
            return Content(year + "/" + month);

        }

        public ActionResult Details(int Id)
        {
            var Movies = _Context.Movies.SingleOrDefault(c => c.Id == Id);
            return View(Movies);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("MovieForm", "Movies", movie);
            }
            try
            {
                if (movie.Id == 0)
                {
                    _Context.Movies.Add(movie);
                }
                else
                {
                    var Movies = _Context.Movies.SingleOrDefault(c => c.Id == movie.Id);
                }
                _Context.SaveChanges();

            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
            }
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult MovieForm()
        {

            Movie movie = new Movie();
         

            return View(movie);
        }




    }
}