using Interfaces.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Interfaces.DBModel
{
    public class DynamicDB : DesignTimeDbContextFactory
    {
        // Getting connection for db
        public static DynamicDBContext ConnectToDb() =>
             new DesignTimeDbContextFactory().CreateDbContext(Array.Empty<string>());
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DynamicDBContext>
    {
        // Creating db context for connect to db
        public DynamicDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DynamicDBContext>();

            builder.UseSqlServer(ConnectionString.connectionString);

            return new DynamicDBContext(builder.Options);
        }
    }
}
