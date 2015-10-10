using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionPlanet.DataAccess.Entities
{
    public class LotImage
    {
        public Guid Id { get; set; }

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }
    }
}
