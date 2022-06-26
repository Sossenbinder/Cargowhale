using System.Data.SqlClient;
using Cargowhale.Contracts.DbExport.Database;
using Cargowhale.Contracts.DbExport.Services;
using Cargowhale.Domain.Sql.Utils;
using Cargowhale.Infrastructure.Core.Async;

namespace Cargowhale.Domain.Sql.Service
{
	public class SqlSnapshotService : IDatabaseExporter
	{
		private readonly AzureStorageBlobPathProvider _azureStorageBlobPathProvider;

		public SqlSnapshotService(AzureStorageBlobPathProvider azureStorageBlobPathProvider)
		{
			_azureStorageBlobPathProvider = azureStorageBlobPathProvider;
		}

		public async Task<DatabaseExportResult> ExportDatabase(DatabaseExportRequest request)
		{
			await using var sqlConnection = new SqlConnection(request.SqlConnectionString);
			await sqlConnection.OpenAsync();

			var blobSasToken = AzureSasTokenProvider.GetSasToken(request.BlobConnectionString);

			var containerPath = _azureStorageBlobPathProvider.GetContainerNameForBackupBlob(request);
			await using var _ = await EstablishAzuriteCredentials(sqlConnection, containerPath, blobSasToken);

			var fullBlobPath = _azureStorageBlobPathProvider.GetFullBlobNameForBackupBlob(request);
			var snapshotCreation = new SqlCommand($@"BACKUP DATABASE [{request.DatabaseName}]
				 TO URL = N'{fullBlobPath}'",
				sqlConnection);
			await snapshotCreation.ExecuteNonQueryAsync();

			return new(fullBlobPath);
		}

		private async Task<IAsyncDisposable> EstablishAzuriteCredentials(SqlConnection sqlConnection, string containerPath, string blobSasToken)
		{
			var dropCredentialsIfNecessaryCommand = new SqlCommand($@"
				IF EXISTS (SELECT * FROM sys.credentials where name = '{containerPath}')
				BEGIN
				   DROP CREDENTIAL [{containerPath}]
				END",
				sqlConnection);

			await dropCredentialsIfNecessaryCommand.ExecuteNonQueryAsync();

			var createCredentialsCommand = new SqlCommand($@"
				CREATE CREDENTIAL [{containerPath}]
				   WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
				   SECRET = '{blobSasToken}'",
				sqlConnection);

			await createCredentialsCommand.ExecuteNonQueryAsync();

			return new AsyncDisposableFunction(async () =>
			{
				await dropCredentialsIfNecessaryCommand.ExecuteNonQueryAsync();
			});
		}
	}
}