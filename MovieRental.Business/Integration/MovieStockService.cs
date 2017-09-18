using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Business.Integration
{
    public class MovieStockService
    {
        public int GetStock(string name)
        {
            //simulate a random stock service
            return new Random().Next(1, 1000);
        }
    }
}
