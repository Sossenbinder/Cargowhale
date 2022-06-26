namespace Cargowhale.Domain.Processing.Service.Interface
{
	public interface IBackupOrchestrator
	{
		Task RunBackup();
	}
}