using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TTC_ShopSolution.Data.EF
{
    public class TTC_ShopDbContextFactory : IDesignTimeDbContextFactory<TTC_ShopDBContext>
    {
        public TTC_ShopDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TTC-ShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<TTC_ShopDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TTC_ShopDBContext(optionsBuilder.Options);
        }
    }
}
