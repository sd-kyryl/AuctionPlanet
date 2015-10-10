using System;

namespace AuctionPlanet.BusinessLogic.Exceptions
{
    public class UnacceptablePriceException : Exception
    {
        public UnacceptablePriceException() : base("The price you specified is unacceptable")
        {

        }
        public UnacceptablePriceException(string message) : base(message)
        {
        }
    }
}
