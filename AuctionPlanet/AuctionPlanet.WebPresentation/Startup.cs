using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuctionPlanet.WebPresentation.Startup))]
namespace AuctionPlanet.WebPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
