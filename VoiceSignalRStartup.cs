using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Collabry;

[assembly: OwinStartup(typeof(VoiceSignalRStartup))]

namespace Collabry
{
    public class VoiceSignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            var hubConfig = new HubConfiguration()
            {
                EnableDetailedErrors = true
            };

            app.MapSignalR("/voicehub", hubConfig);
        }
    }

}
