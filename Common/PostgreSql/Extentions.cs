using Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.PostgreSql
{
    public static class Extentions
    {
        public static IServiceCollection AddPostgre(this IServiceCollection services) {
            services.AddDbContext<>((serviceProvider, options) => {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(PostgreSqlSettings.ConnectionString);
                options.UseNpgSql(connectionString);
            });

            return services;
        }
    }
}
