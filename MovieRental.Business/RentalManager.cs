using MovieRental.Business.Integration;
using MovieRental.Business.TestFlask;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFlask.Aspects;

namespace MovieRental.Business
{
    public class RentalManager
    {
        private readonly MovieInfoService infoService;
        private readonly MovieStockService stockService;

        public RentalManager(MovieInfoService infoService, MovieStockService stockService)
        {
            this.infoService = infoService;
            this.stockService = stockService;
        }

        [Playback(typeof(MovieNameIdentifier))]
        public Movie GetMovieWithStockCount(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));
            //gets movie info from info service
            var movie = infoService.GetMovieInfo(name);
            //obtain stock info from stock service
            movie.StockCount = stockService.GetStock(name);
            
            return movie;
        }

        [Playback]
        public Movie GetRandomMovie()
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));
            //gets movie info from info service
            Movie movie = infoService.GetRandomMovieInfo();
            //obtain stock info from stock service
            movie.StockCount = stockService.GetStock(movie.Name);

            return movie;
        }

        [Playback]
        public void ResetMovie(string name)
        {
            var movie = GetMovieWithStockCount(name);
            movie.StockCount = 0;
        }

        [Playback]
        public void CheckStatus()
        {
            Debug.WriteLine("Status OK!");
        }
    }
}
