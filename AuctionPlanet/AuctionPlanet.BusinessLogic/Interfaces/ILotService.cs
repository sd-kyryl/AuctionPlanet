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

        void CreateLot(LotInfo lotInfo);
        void ApproveALot(Guid id);
        void BidOnALot(Guid id, decimal newPrice, string newBidder);
        void DisposeOfExpiredLots();
        void Dispose();
    }
}
