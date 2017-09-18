using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRental.Models;

namespace MovieRental.Business.Integration
{
    public class MovieScoreService
    {
        internal Score GetScore(string name)
        {
            //simulate a random score service
            return new Score
            {
                Rating = new Random().NextDouble() * 10,
                Source = (RatingSource)new Random().Next(0, 2)
            };
        }
    }
}
