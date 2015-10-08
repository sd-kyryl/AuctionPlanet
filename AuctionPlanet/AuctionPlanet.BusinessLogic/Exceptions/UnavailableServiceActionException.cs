using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionPlanet.BusinessLogic.Exceptions
{
    public class UnavailableServiceActionException : Exception
    {
        public UnavailableServiceActionException() : base("The service action you tried to perform is no longer available")
        {
            
        }
        public UnavailableServiceActionException(string message) : base(message)
        {
        }
    }
}
