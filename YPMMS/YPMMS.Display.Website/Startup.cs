using AutoMapper;
using YPMMS.Display.Utilities;
using YPMMS.Display.Website.AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using NLog;
using Owin;
using YPMMS.Data.Repository.AutoMapper;

[assembly: OwinStartup(typeof(YPMMS.Display.Website.Startup))]
namespace YPMMS.Display.Website
{
    public partial class Startup
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            Log.Info("Startup.Configuration() called");

            ConfigureAuth(app);

           // app.MapSignalR();

            // Register JSON settings to ensure enums are serialised as strings
            var jsonSerializer = JsonSerializer.Create(Consts.JsonSettings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => jsonSerializer);

            //AutoMapper setup
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapper.AutoMapperProfile>();
                cfg.AddProfile<Data.Repository.AutoMapper.AutoMapperProfile>();
            });
        }
    }
}
