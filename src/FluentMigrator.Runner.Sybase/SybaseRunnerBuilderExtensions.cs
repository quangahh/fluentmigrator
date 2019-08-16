using FluentMigrator.Runner.BatchParser;
using FluentMigrator.Runner.Generators.Sybase;
using FluentMigrator.Runner.Processors.Sybase;

using Microsoft.Extensions.DependencyInjection;

namespace FluentMigrator.Runner
{
    /// <summary>
    /// Extension methods for <see cref="IMigrationRunnerBuilder"/>
    /// </summary>
    public static class SybaseRunnerBuilderExtensions
    {
        /// <summary>
        /// Adds Sybase support
        /// </summary>
        /// <param name="builder">The builder to add the Sybase-specific services to</param>
        /// <returns>The migration runner builder</returns>
        public static IMigrationRunnerBuilder AddSybase(this IMigrationRunnerBuilder builder)
        {
            builder.Services
                .AddTransient<SybaseBatchParser>()
                .AddScoped<SybaseDbFactory>()
                .AddScoped<SybaseProcessor>()
                .AddScoped<IMigrationProcessor>(sp => sp.GetRequiredService<SybaseProcessor>())
                .AddScoped<SybaseQuoter>()
                .AddScoped<SybaseGenerator>()
                .AddScoped<IMigrationGenerator>(sp => sp.GetRequiredService<SybaseGenerator>());
            return builder;
        }
    }
}
