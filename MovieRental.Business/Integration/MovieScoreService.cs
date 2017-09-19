using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRental.Models;
using TestFlask.Aspects;
using MovieRental.Business.TestFlask;
using System.Threading;

namespace MovieRental.Business.Integration
{
    public class MovieScoreService
    {
        [Playback(typeof(MovieNameIdentifier))]
        internal Score GetScore(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));
            //simulate a random score service
            return new Score
            {
                Rating = new Random().NextDouble() * 10,
                Source = (RatingSource)new Random().Next(0, 2)
            };
        }
    }
}
