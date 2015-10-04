using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.DataAccess.Entities;
using static AutoMapper.Mapper;

namespace AuctionPlanet.WebPresentation
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateMap();
        }

        private static void CreateMap()
        {
            CreateMap<Lot, LotInfo>();
            CreateMap<LotInfo, Lot>();
            CreateMap<IEnumerable<Lot>, IEnumerable<LotInfo>>();
            CreateMap<IEnumerable<LotInfo>, IEnumerable<Lot>>();

            CreateMap<AuctionUser, AuctionUserInfo>();
            CreateMap<AuctionUserInfo, AuctionUser>();
            CreateMap<IEnumerable<AuctionUser>, IEnumerable<AuctionUserInfo>>();
            CreateMap<IEnumerable<AuctionUserInfo>, IEnumerable<AuctionUser>>();
        }
    }
}
