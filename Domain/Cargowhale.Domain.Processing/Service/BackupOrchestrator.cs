using Cargowhale.Contracts.DbExport.Database;
using Cargowhale.Contracts.DbExport.Services;
using Cargowhale.Domain.Processing.Input;
using Cargowhale.Domain.Processing.Service.Interface;
using Cargowhale.Infrastructure.Core.Docker;
using Microsoft.Extensions.Logging;

namespace Cargowhale.Domain.Processing.Service
{
	public class BackupOrchestrator : IBackupOrchestrator
	{
		private readonly ILogger<BackupOrchestrator> _logger;

		private readonly IDatabaseExporter _databaseExporter;

		private readonly IDockerDatabaseProvider _dockerDatabaseProvider;

		private readonly ArgumentProvider _argumentProvider;

		public BackupOrchestrator(
			ILogger<BackupOrchestrator> logger,
			IDatabaseExporter databaseExporter,
			IDockerDatabaseProvider dockerDatabaseProvider,
			ArgumentProvider argumentProvider)
		{
			_logger = logger;
			_databaseExporter = databaseExporter;
			_dockerDatabaseProvider = dockerDatabaseProvider;
			_argumentProvider = argumentProvider;
		}

		public async Task RunBackup()
		{
			var exportRequest = _argumentProvider.RequestArguments;

			var exportResult = await _databaseExporter.ExportDatabase(exportRequest);

			var dockerBuildResult = await _dockerDatabaseProvider.BuildDockerfile(exportResult, exportRequest);

			await PushImage(exportRequest, dockerBuildResult);
		}

		private async Task PushImage(DatabaseExportRequest request, BuiltDockerfileInformation buildInformation)
		{
			await DockerCli.Run(_logger, "login", "-u", request.DockerUsername, "-p", request.DockerPassword);
			await DockerCli.Run(_logger, "push", buildInformation.ImageTag);
		}
	}
}