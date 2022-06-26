using Cargowhale.Contracts.DbExport.Database;

namespace Cargowhale.Contracts.DbExport.Services
{
	public interface IDatabaseExporter
	{
		public Task<DatabaseExportResult> ExportDatabase(DatabaseExportRequest request);
	}
}