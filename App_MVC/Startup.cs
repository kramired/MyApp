using Microsoft.AspNetCore.Builder;

namespace App_MVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            app.UseSignalR(routes => routes.MapHub<NotificationHub>("/notificationHub"));
        }
    }
}
