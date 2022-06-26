using Cargowhale.Contracts.DbExport.Database;

namespace Cargowhale.Contracts.DbExport.Services
{
	public interface IDockerDatabaseProvider
	{
		ValueTask<BuiltDockerfileInformation> BuildDockerfile(DatabaseExportResult databaseExportResult, DatabaseExportRequest exportRequest);
	}
}