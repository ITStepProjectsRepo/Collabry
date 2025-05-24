using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Collabry;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(TextSignalRStartup))]

namespace Collabry
{
    public class TextSignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            var hubConfig = new HubConfiguration()
            {
                EnableDetailedErrors = true
            };

            app.MapSignalR("/texthub", hubConfig);
        }
    }
}
