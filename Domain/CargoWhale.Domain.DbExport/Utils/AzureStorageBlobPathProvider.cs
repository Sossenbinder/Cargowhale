using Cargowhale.Contracts.DbExport.Database;
using Cargowhale.Domain.Sql.Configuration;
using Microsoft.Extensions.Options;

namespace Cargowhale.Domain.Sql.Utils
{
	public class AzureStorageBlobPathProvider
	{
		private readonly SqlExportOptions _sqlExportOptions;

		public AzureStorageBlobPathProvider(IOptions<SqlExportOptions> sqlExportOptions)
		{
			_sqlExportOptions = sqlExportOptions.Value;
		}

		public string GetFullBlobNameForBackupBlob(DatabaseExportRequest request)
			=> Path.Combine(request.ContainerUrl, _sqlExportOptions.ContainerName, GetBlobName(request.DatabaseName));

		public string GetContainerNameForBackupBlob(DatabaseExportRequest request)
			=> Path.Combine(request.ContainerUrl, _sqlExportOptions.ContainerName);

		private static string GetBlobName(string databaseName)
		{
			var date = DateTime.UtcNow;

			return $"{databaseName}_{date:yyyyMMddhhmmss}.bak";
		}
	}
}