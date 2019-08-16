using System;
using System.Data.Common;

using FluentMigrator.Runner.Generators.Sybase;
using FluentMigrator.Runner.Initialization;

using JetBrains.Annotations;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluentMigrator.Runner.Processors.Sybase
{
    public class SybaseProcessor : SybaseProcessorBase
    {
        /// <inheritdoc />
        public SybaseProcessor(
            [NotNull] SybaseDbFactory factory,
            [NotNull] SybaseGenerator generator,
            [NotNull] ILogger<SybaseProcessor> logger,
            [NotNull] IOptionsSnapshot<ProcessorOptions> options,
            [NotNull] IConnectionStringAccessor connectionStringAccessor,
            [NotNull] IServiceProvider serviceProvider)
            : base(
                () => factory.Factory,
                generator,
                logger,
                options,
                connectionStringAccessor,
                serviceProvider)
        {
        }

        /// <inheritdoc />
        public SybaseProcessor([NotNull] Func<DbProviderFactory> factoryAccessor, [NotNull] IMigrationGenerator generator, [NotNull] ILogger logger, [NotNull] IOptionsSnapshot<ProcessorOptions> options, [NotNull] IConnectionStringAccessor connectionStringAccessor, [NotNull] IServiceProvider serviceProvider) : base(factoryAccessor, generator, logger, options, connectionStringAccessor, serviceProvider)
        {
        }
    }
}
