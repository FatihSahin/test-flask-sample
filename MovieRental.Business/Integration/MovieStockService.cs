using MovieRental.Business.TestFlask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFlask.Aspects;

namespace MovieRental.Business.Integration
{
    public class MovieStockService
    {
        [Playback(typeof(MovieNameIdentifier))]
        public int GetStock(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));
            //simulate a random stock service
            return new Random().Next(1, 1000);
        }
    }
}
