using System;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.BusinessLogic.Repositories
{
    public class DatabaseUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private LotRepository _lotRepository;
        private bool _disposed;

        public DatabaseUnitOfWork()
        {
            _context = ApplicationDbContext.Create();
        }

        public IRepository<Lot> Lots => _lotRepository ?? (_lotRepository = new LotRepository(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
