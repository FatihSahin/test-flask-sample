using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Models
{
    public class Score
    {
        public double Rating { get; set; }

        public RatingSource Source { get; set; }
    }

    public enum RatingSource
    {
        Imdb,
        RottenTomatoes
    }
}
