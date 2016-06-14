using System;
using System.Collections.Generic;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.BusinessLogic.Exceptions;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Utility;
using static AutoMapper.Mapper;

namespace AuctionPlanet.BusinessLogic.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _database;

        public LotService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public IEnumerable<LotInfo> GetCreatedLots(string userName)
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => string.Equals(lot.OriginalOwner, userName)));
        }

        public IEnumerable<LotInfo> GetBoughtLots(string userName)
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => string.Equals(lot.CurrentBidder, userName) && lot.Status == LotStatus.Sold));
        }

        public IEnumerable<LotInfo> GetCurrentlyHeldLots(string userName)
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => string.Equals(lot.CurrentBidder, userName) && lot.Status == LotStatus.Available));
        }

        public void CreateLot(LotInfo lotInfo)
        {
            _database.Lots.Create(Map<Lot>(lotInfo));
            _database.Save();
        }

        public LotInfo GetLotInfo(Guid id)
        {
            return Map<LotInfo>(_database.Lots.Get(id));
        }

        public IEnumerable<LotInfo> GetLotInfos()
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.GetAll());
        }

        public void ApproveALot(Guid id)
        {
            Lot lot = _database.Lots.Get(id);

            if (lot.Status != LotStatus.PendingApproval)
            {
                throw new UnavailableServiceActionException();
            }

            lot.StartTime = DateTime.Now;
            lot.Status = LotStatus.Available;
            _database.Lots.Update(lot);
            _database.Save();
        }

        public void RenewTheLot(Guid id)
        {
            Lot lot = _database.Lots.Get(id);

            if (lot.Status != LotStatus.Expired)
            {
                throw new UnavailableServiceActionException();
            }

            lot.StartTime = DateTime.Now;
            lot.Status = LotStatus.Available;
            _database.Lots.Update(lot);
            _database.Save();
        }

        public IEnumerable<LotInfo> GetPendingLots()
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.PendingApproval));
        }

        public IEnumerable<LotInfo> GetAvailableLots()
        {
            IEnumerable<Lot> lots = _database.Lots.Find(lot => lot.Status == LotStatus.Available);
            return Map<IEnumerable<LotInfo>>(lots);
        }

        public IEnumerable<LotInfo> GetSoldLots()
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.Sold));
        }

        public IEnumerable<LotInfo> GetExpiredLots()
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.Expired));
        }

        public void BidOnALot(Guid id, decimal newPrice, string newBidder)
        {
            Lot lot = _database.Lots.Get(id);

            if (lot.Status != LotStatus.Available)
            {
                throw new UnavailableServiceActionException();
            }

            if (lot.CurrentPrice >= newPrice)
            {
                throw new UnacceptablePriceException();
            }

            lot.CurrentPrice = newPrice;
            lot.CurrentBidder = newBidder;
            _database.Lots.Update(lot);
            _database.Save();
        }

        public void DisposeOfExpiredLots()
        {
            IEnumerable<Lot> lots = _database.Lots.Find(lot => lot.Status == LotStatus.Available);

            foreach (Lot lot in lots)
            {
                if (lot.StartTime != null && (lot.StartTime.Value.AddTicks(lot.Duration) - DateTime.Now).Ticks < 0L)
                {
                    lot.Status = string.IsNullOrEmpty(lot.CurrentBidder) ? LotStatus.Expired : LotStatus.Sold;
                    _database.Lots.Update(lot);
                }
            }

            _database.Save();
        }

        public IEnumerable<LotInfo> SearchLotInfos(string searchQuery)
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.Available && lot.Title.Contains(searchQuery)));
        }

        public void DeleteLot(Guid id)
        {
            _database.Lots.Delete(id);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
