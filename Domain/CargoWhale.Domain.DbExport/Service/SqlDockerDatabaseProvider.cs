using System.Text;
using Cargowhale.Contracts.DbExport.Database;
using Cargowhale.Contracts.DbExport.Services;
using Cargowhale.Domain.Sql.Utils;
using Ductus.FluentDocker.Builders;

namespace Cargowhale.Domain.Sql.Service
{
	public class SqlDockerDatabaseProvider : IDockerDatabaseProvider
	{
		public ValueTask<BuiltDockerfileInformation> BuildDockerfile(DatabaseExportResult databaseExportResult, DatabaseExportRequest exportRequest)
		{
			var imageBuilder = new Builder()
				.DefineImage($"{exportRequest.ImageName}:{exportRequest.Tag ?? "latest"}")
				.From("mcr.microsoft.com/mssql/server")
				.Environment($"SA_PASSWORD={exportRequest.DatabasePassword}")
				.Environment("ACCEPT_EULA=Y");

			var restoreBakCommand = BuildRunCommand(databaseExportResult, exportRequest);

			using var fileBuilder = imageBuilder
				.Run(restoreBakCommand)
				.Build();

			return ValueTask.FromResult(new BuiltDockerfileInformation($"{fileBuilder.Name}:{fileBuilder.Tag}"));
		}

		private static string BuildRunCommand(DatabaseExportResult databaseExportResult, DatabaseExportRequest exportRequest)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("(/opt/mssql/bin/sqlservr &) | grep -q \"Service Broker manager has started\" && /opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U '");
			stringBuilder.Append(exportRequest.DatabaseUsername);
			stringBuilder.Append("' -P '");
			stringBuilder.Append(exportRequest.DatabasePassword);
			stringBuilder.Append("' -Q \"");
			stringBuilder.Append($"CREATE CREDENTIAL [{exportRequest.ContainerUrl}] ");
			stringBuilder.Append("WITH IDENTITY = 'SHARED ACCESS SIGNATURE', ");
			stringBuilder.Append($"SECRET = '{AzureSasTokenProvider.GetSasToken(exportRequest.BlobConnectionString)}' ");
			stringBuilder.Append("RESTORE DATABASE ");
			stringBuilder.Append(exportRequest.DatabaseName);
			stringBuilder.Append(" FROM URL = '");
			stringBuilder.Append(databaseExportResult.ExportPath);
			stringBuilder.Append($"' WITH REPLACE; DROP CREDENTIAL [{exportRequest.ContainerUrl}]\" && pkill sqlservr");
			return stringBuilder.ToString();
		}
	}
}