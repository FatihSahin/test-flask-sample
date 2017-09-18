using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Business.Integration
{
    public class MovieInfoService
    {
        private string[] directors = new string[] { "Guy Ritchie", "Quentin Tarantino", "Steven Spielberg", "Client Eastwood", "Christopher Nolan" };
        private readonly MovieScoreService scoreService;

        public MovieInfoService(MovieScoreService scoreService)
        {
            //injected score service
            this.scoreService = scoreService;
        }

        public Movie GetInfo(string name)
        {
            //Simulate a random movie info service with random year and director data and id
            Movie movie = new Movie
            {
                Id = new Random().Next(1000, 6000),
                Director = directors[new Random().Next(0, directors.Length)],
                ReleaseYear = new Random().Next(2001, 2018),
                Name = name
            };

            //sub call to obtain score
            movie.Score = scoreService.GetScore(name);

            return movie;
        }
    }
}
