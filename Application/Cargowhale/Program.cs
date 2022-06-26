using Cargowhale.Domain.Processing.DI;
using Cargowhale.Domain.Processing.Input;
using Cargowhale.Domain.Processing.Service;
using Cargowhale.Domain.Sql.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection()
	.AddLogging(loggingBuilder =>
	{
		loggingBuilder.AddConsole();
	})
	.AddProcessingModule()
	.AddSqlModule();

await using var serviceProvider = serviceCollection.BuildServiceProvider();

var timer = serviceProvider.GetRequiredService<ExportTimer>();

var argumentProvider = serviceProvider.GetRequiredService<ArgumentProvider>();
argumentProvider.LoadArguments(args);

await timer.Start(argumentProvider.CronExpression);

await Task.Delay(-1);