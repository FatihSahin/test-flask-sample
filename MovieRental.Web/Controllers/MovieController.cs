using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Web.Controllers
{
    public class MovieController : Controller
    {
        private MovieRentalService.MovieRentalServiceClient serviceClient;

        public MovieController()
        {
            serviceClient = new MovieRentalService.MovieRentalServiceClient();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Movie movie)
        {
            try
            {
                serviceClient.AddMovie(movie);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Rent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Rent(Movie movie)
        {
            try
            {
                serviceClient.RentMovie(movie.Name);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Movie movie)
        {
            try
            {
                serviceClient.DeleteMovie(movie.Name);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
