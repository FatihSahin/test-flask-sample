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
        private readonly MovieInfoService infoService;

        public MovieRentalService()
        {
            MovieScoreService scoreService = new MovieScoreService(); 
            MovieStockService stockService = new MovieStockService();

            infoService = new MovieInfoService(scoreService);
            rentalMananger = new RentalManager(infoService, stockService);
        }

        [Playback(typeof(MovieNameIdentifier))]
        public Movie GetMovie(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            return rentalMananger.GetMovieWithStockCount(name);
        }

        [Playback]
        public Movie AddMovie(Movie movie)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            return infoService.AddMovieInfo(movie);
        }

        [Playback]
        public bool DeleteMovie(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            return infoService.DeleteMovieInfo(name);
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
