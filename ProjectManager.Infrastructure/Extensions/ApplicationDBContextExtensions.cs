using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Infrastructure.Extensions
{
    public static class ApplicationDBContextExtensions
    {
        public static void AddApplicationContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDBContext>(optionsAction =>
                optionsAction.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        }
    }
}
