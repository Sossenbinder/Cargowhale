using Cargowhale.Contracts.DbExport.Services;
using Cargowhale.Domain.Sql.Configuration;
using Cargowhale.Domain.Sql.Service;
using Cargowhale.Domain.Sql.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cargowhale.Domain.Sql.DI
{
	public static class SqlModule
	{
		public static IServiceCollection AddSqlModule(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddSingleton<IDatabaseExporter, SqlSnapshotService>()
				.AddSingleton<IDockerDatabaseProvider, SqlDockerDatabaseProvider>()
				.AddSingleton<AzureStorageBlobPathProvider>()
				.Configure<SqlExportOptions>(options =>
				{
					options.ContainerName = SqlExportDefinitions.ContainerName;
				});
		}
	}
}