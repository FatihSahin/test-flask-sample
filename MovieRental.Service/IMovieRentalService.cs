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
    }
}
