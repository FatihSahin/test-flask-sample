using MovieRental.Business.Integration;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Movie GetMovie(string name)
        {
            //gets movie info from info service
            var movie = infoService.GetInfo(name);
            //obtain stock info from stock service
            movie.StockCount = stockService.GetStock(name);
            
            return movie;
        }
    }
}
