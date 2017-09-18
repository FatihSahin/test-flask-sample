using MovieRental.Business;
using MovieRental.Business.Integration;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
            
            this.rentalMananger = new RentalManager(infoService, stockService);
        }


        public Movie GetMovie(string name)
        {
            return rentalMananger.GetMovie(name);
        }

        public RentalResult RentMovie(string name)
        {
            var movie = GetMovie(name);
            movie.StockCount--;
            return new RentalResult
            {
                IsSuccess = true,
                Message = "OK"
            };
        }
    }
}
