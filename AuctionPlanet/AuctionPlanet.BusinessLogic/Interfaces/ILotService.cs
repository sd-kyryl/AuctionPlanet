using System;
using System.Collections.Generic;
using AuctionPlanet.BusinessLogic.DataTransferObjects;

namespace AuctionPlanet.BusinessLogic.Interfaces
{
    public interface ILotService
    {
        LotInfo GetLotInfo(Guid id);
        IEnumerable<LotInfo> GetLotInfos();
        IEnumerable<LotInfo> GetPendingLots();
        IEnumerable<LotInfo> GetAvailableLots();
        IEnumerable<LotInfo> GetExpiredLots();
        IEnumerable<LotInfo> GetSoldLots();
        IEnumerable<LotInfo> SearchLotInfos(string searchQuery);
        IEnumerable<LotInfo> GetCurrentlyHeldLots(string userName);
        IEnumerable<LotInfo> GetBoughtLots(string userName);
        IEnumerable<LotInfo> GetCreatedLots(string userName);

        void CreateLot(LotInfo lotInfo);
        void ApproveALot(Guid id);
        void RenewTheLot(Guid id);
        void BidOnALot(Guid id, decimal newPrice, string newBidder);
        void DisposeOfExpiredLots();
        void DeleteLot(Guid id);
        void Dispose();
    }
}
