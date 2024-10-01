using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecetasService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Start listening to RabbitMQ in a separate thread to avoid blocking the application startup
            var listenerThread = new System.Threading.Thread(() => RabbitMQListener.StartListening("RecetaQueue"));
            listenerThread.IsBackground = true; // Ensures the thread will not prevent application exit
            listenerThread.Start();
        }
    }
}
