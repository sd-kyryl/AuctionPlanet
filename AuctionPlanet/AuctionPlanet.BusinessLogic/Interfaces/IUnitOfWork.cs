using System;
using AuctionPlanet.DataAccess.Entities;

namespace AuctionPlanet.BusinessLogic.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Lot> Lots { get; }
        void Save();
    }
}
