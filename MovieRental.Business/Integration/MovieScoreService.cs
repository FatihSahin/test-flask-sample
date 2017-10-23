using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRental.Models;
using TestFlask.Aspects;
using MovieRental.Business.TestFlask;
using System.Threading;
using System.Net.Http;
using TestFlask.Assistant.Core.WebApi;
using TestFlask.Assistant.Core.Config;

namespace MovieRental.Business.Integration
{
    public class MovieScoreService
    {
        private readonly TestFlaskAssistantConfig assistantConfig;

        public MovieScoreService()
        {
            assistantConfig = TestFlaskAssistantConfig.Instance;
        }

        [Playback(typeof(MovieNameIdentifier))]
        internal Score GetScore(string name)
        {
            //simulate a delay
            Thread.Sleep(new Random().Next(500, 2000));

            if (name.Length % 2 == 0) //to demonstrate a inner score api to pass incoming TestFlask headers, 
                //if movie name length is odd, fetch score from our ForgottenPotatoes API
            {
                double score = 0;

                //Here we enable test flask assistant core client message handler 
                //to pass incoming test-flask headers from MVC app to our external score API backend
                HttpClient httpClient = HttpClientFactory.Create(new TestFlaskMessageHandler());
                httpClient.BaseAddress = new Uri("http://localhost:64283/");

                var scoreRes = httpClient.GetAsync($"api/score/{name}").Result;

                if (scoreRes.IsSuccessStatusCode)
                {
                    score = scoreRes.Content.ReadAsAsync<double>().Result;
                }

                return new Score
                {
                    Rating = score,
                    Source = RatingSource.ForgottenPotatoes
                };
            }
            else
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
}
