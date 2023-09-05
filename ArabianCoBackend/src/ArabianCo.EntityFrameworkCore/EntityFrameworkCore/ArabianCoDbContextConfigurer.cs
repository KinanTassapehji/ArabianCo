using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ArabianCo.EntityFrameworkCore
{
    public static class ArabianCoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ArabianCoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ArabianCoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
