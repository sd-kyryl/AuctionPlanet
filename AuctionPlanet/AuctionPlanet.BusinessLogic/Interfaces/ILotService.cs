using System;
using System.Collections.Generic;
using AuctionPlanet.BusinessLogic.DataTransferObjects;

namespace AuctionPlanet.BusinessLogic.Interfaces
{
    public interface ILotService
    {
        void CreateLot(LotInfo lotInfo);

        LotInfo GetLotInfo(Guid id);
        IEnumerable<LotInfo> GetLotInfos();

        void ApproveALot(Guid id);
        IEnumerable<LotInfo> GetPendingLots();
        IEnumerable<LotInfo> GetAvailableLots();
        IEnumerable<LotInfo> GetSoldLots();

        void BidOnALot(Guid id, decimal newPrice, string newBidder);
        IEnumerable<LotInfo> SearchLotInfos(string searchQuery);
        void Dispose();
    }
}
