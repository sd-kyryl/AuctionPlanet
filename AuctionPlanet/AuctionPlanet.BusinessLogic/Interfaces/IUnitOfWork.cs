using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPlanet.DataAccess.Entities;

namespace AuctionPlanet.BusinessLogic.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Lot> Lots { get; }
        IRepository<AuctionUser> AuctionUsers { get; }
        void Save();
    }
}
