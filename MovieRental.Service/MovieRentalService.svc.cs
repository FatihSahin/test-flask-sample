using MovieRental.Business;
using MovieRental.Business.Integration;
using MovieRental.Business.TestFlask;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using TestFlask.Aspects;

namespace MovieRental.Service
{
    public class MovieRentalService : IMovieRentalService
    {
        private readonly RentalManager rentalMananger;

        public MovieRentalService()
        {
            MovieScoreService scoreService = new MovieScoreService();
            MovieInfoService infoService = new MovieInfoService(scoreService);
            MovieStockService stockService = new MovieStockService();

            rentalMananger = new RentalManager(infoService, stockService);
        }

        [Playback(typeof(MovieNameIdentifier))]
        public Movie GetMovie(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            //Simulate a buggy movie record
            if (name == "A Bug's Life")
            {
                throw new ApplicationException("Oh my god, this is a bug indeed.");
            }

            return rentalMananger.GetMovie(name);
        }

        [Playback(typeof(MovieNameIdentifier))]
        public RentalResult RentMovie(string name)
        {
            var movie = GetMovie(name);

            movie.StockCount--;

            return new RentalResult
            {
                IsSuccess = true,
                Message = "OK",
                Movie = movie
            };
        }
    }
}
