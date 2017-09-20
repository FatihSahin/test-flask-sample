using MovieRental.Business.TestFlask;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFlask.Aspects;

namespace MovieRental.Business.Integration
{
    public class MovieInfoService
    {
        private string[] directors = new string[] { "Guy Ritchie", "Quentin Tarantino", "Steven Spielberg", "Client Eastwood", "Christopher Nolan" };

        private static List<Movie> movies;

        static MovieInfoService()
        {
            movies = new List<Movie>
            {
                new Movie
                {
                    Name= "Memento",
                },
                new Movie
                {
                    Name = "Snatch"
                },
                new Movie
                {
                    Name = "Jurassic Park"
                },
                new Movie
                {
                    Name = "Pulp Fiction"
                }
            };
        }

        private readonly MovieScoreService scoreService;

        public MovieInfoService(MovieScoreService scoreService)
        {
            //injected score service
            this.scoreService = scoreService;
        }

        [Playback]
        public Movie AddMovieInfo(Movie movie)
        {
            movies.Add(movie);
            return GetMovieInfo(movie.Name);
        }

        [Playback(typeof(MovieNameIdentifier))]
        public bool DeleteMovieInfo(string name)
        {
            var existingMovie = movies.SingleOrDefault(mv => mv.Name == name);
            if (existingMovie != null)
            {
                movies.Remove(existingMovie);
                return true;
            }

            return false;
        }

        [Playback(typeof(MovieNameIdentifier))]
        public Movie GetMovieInfo(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            //Simulate a random movie info service with random year and director data and id (think about it as someone is always altering unreliable test db)
            var movie = movies.SingleOrDefault(mv => mv.Name == name);

            if (movie == null)
                throw new ApplicationException("Movie was not found");

            movie.Director = directors[new Random().Next(0, directors.Length)];
            movie.ReleaseYear = new Random().Next(2001, 2018);
            movie.Id = new Random().Next(1000, 6000);


            //sub call to obtain score
            movie.Score = scoreService.GetScore(name);

            return movie;
        }
    }
}
