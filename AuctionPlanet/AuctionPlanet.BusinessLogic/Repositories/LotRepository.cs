using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.BusinessLogic.Repositories
{
    public class LotRepository : IRepository<Lot>
    {
        private readonly ApplicationDbContext _context;

        public LotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lot> GetAll()
        {
            return _context.Lots.ToList();
        }

        public Lot Get(Guid id)
        {
            return _context.Lots.Find(id);
        }

        public IEnumerable<Lot> Find(Func<Lot, bool> predicate)
        {
            return _context.Lots.Where(predicate).ToList();
        }

        public void Create(Lot item)
        {
            _context.Lots.Add(item);
        }

        public void Update(Lot item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Lot lot = _context.Lots.Find(id);

            if (lot != null)
            {
                _context.Lots.Remove(lot);
            }
        }
    }
}
