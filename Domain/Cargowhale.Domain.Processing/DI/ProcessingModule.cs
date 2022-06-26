using Cargowhale.Domain.Processing.Input;
using Cargowhale.Domain.Processing.Service;
using Cargowhale.Domain.Processing.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Cargowhale.Domain.Processing.DI
{
	public static class ProcessingModule
	{
		public static IServiceCollection AddProcessingModule(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddSingleton<IBackupOrchestrator, BackupOrchestrator>()
				.AddSingleton<ArgumentProvider>()
				.AddSingleton<ExportTimer>();
		}
	}
}