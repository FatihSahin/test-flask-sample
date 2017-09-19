using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public string Director { get; set; }

        public int StockCount { get; set; }

        public Score Score { get; set; }
    }
}
