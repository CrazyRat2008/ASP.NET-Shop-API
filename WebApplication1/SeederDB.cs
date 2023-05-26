using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;

namespace WebApplication1
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<ApplicationDBContext>();
                context.Database.Migrate(); 
            }
        }
    }
}
