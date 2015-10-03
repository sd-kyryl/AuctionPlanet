using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.BusinessLogic.Repositories
{
    public class AuctionUserRepository : IRepository<AuctionUser>
    {
        private readonly ApplicationDbContext _context;

        public AuctionUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AuctionUser> GetAll()
        {
            return _context.AuctionUsers;
        }

        public AuctionUser Get(Guid id)
        {
            return _context.AuctionUsers.Find(id);
        }

        public IEnumerable<AuctionUser> Find(Func<AuctionUser, bool> predicate)
        {
            return _context.AuctionUsers.Where(predicate).ToList();
        }

        public void Create(AuctionUser item)
        {
            _context.AuctionUsers.Add(item);
        }

        public void Update(AuctionUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            AuctionUser actionUser = _context.AuctionUsers.Find(id);

            if (actionUser != null)
            {
                _context.AuctionUsers.Remove(actionUser);
            }
        }
    }
}
