using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieRental.Service
{ 
    [ServiceContract]
    public interface IMovieRentalService
    {
        [OperationContract]
        Movie GetMovie(string name);

        [OperationContract]
        RentalResult RentMovie(string name);

        [OperationContract]
        Movie AddMovie(Movie movie);

        [OperationContract]
        bool DeleteMovie(string name);

        //An example of a no args function
        [OperationContract]
        Movie RandomMovie();

        //An example of an action
        [OperationContract]
        void ResetMovie(Movie movie);

        //An example of no args action
        [OperationContract]
        void HealthCheck();
    }
}
