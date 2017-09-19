using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFlask.Aspects.Identifiers;

namespace MovieRental.Business.TestFlask
{
    public class MovieNameIdentifier : IRequestIdentifier<string>
    {
        public string ResolveDisplayInfo(string name)
        {
            //return a value that will be displayed on test flask UI to ease understanding what is actually represented by request args.
            return $"Movie: {name}";
        }

        public string ResolveIdentifierKey(string name)
        {
            //return a value that represent request args as a unique val
            return name;
        }
    }
}
