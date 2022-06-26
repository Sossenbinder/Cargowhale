using Cargowhale.Domain.Processing.Service.Interface;
using Sgbj.Cron;

namespace Cargowhale.Domain.Processing.Service
{
	public class ExportTimer
	{
		private readonly IBackupOrchestrator _backupOrchestrator;

		public ExportTimer(IBackupOrchestrator backupOrchestrator)
		{
			_backupOrchestrator = backupOrchestrator;
		}

		public async Task Start(string cron)
		{
			var periodicTimer = new CronTimer(cron);
			while (await periodicTimer.WaitForNextTickAsync())
			{
				await _backupOrchestrator.RunBackup();
			}
		}
	}
}