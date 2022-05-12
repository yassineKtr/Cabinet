﻿using DataAccess.DbAccess;

namespace Controllers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UsePostgreSql(this IServiceCollection services,
                            IConfiguration configuration)
            => services.Configure<PostgresqlConfig>(configuration);
    }
}
