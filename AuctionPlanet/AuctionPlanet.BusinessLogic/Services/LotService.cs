using System;
using System.Collections.Generic;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
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
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.Available));
        }

        public IEnumerable<LotInfo> GetSoldLots()
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Status == LotStatus.Sold));
        }

        public void BidOnALot(Guid id, decimal newPrice, string newBidder)
        {
            Lot lot = _database.Lots.Get(id);
            lot.CurrentPrice = newPrice;
            lot.CurrentBidder = newBidder;
            _database.Lots.Update(lot);
            _database.Save();
        }

        public IEnumerable<LotInfo> SearchLotInfos(string searchQuery)
        {
            return Map<IEnumerable<LotInfo>>(_database.Lots.Find(lot => lot.Title.Contains(searchQuery)));
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
