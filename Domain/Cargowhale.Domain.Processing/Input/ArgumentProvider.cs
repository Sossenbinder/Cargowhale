using Cargowhale.Contracts.DbExport.Database;
using CommandLine;
using Newtonsoft.Json;

namespace Cargowhale.Domain.Processing.Input
{
	public class ArgumentProvider
	{
		public DatabaseExportRequest RequestArguments { get; private set; } = default!;

		public string CronExpression { get; private set; } = default!;

		public void LoadArguments(string[] cliArgs)
		{
			Parser.Default.ParseArguments<InputArguments>(cliArgs)
				.WithParsed(args =>
				{
					if (args.ConfigFileName is not null)
					{
						var jsonConfig = FromJsonConfig(args.ConfigFileName);
						CronExpression = jsonConfig.Cron;
						RequestArguments = jsonConfig.ExportOptions.ToExportRequest();
					}
					else
					{
						CronExpression = args.Cron;
						RequestArguments = args.ToExportRequest();
					}
				});
		}

		private static JsonConfig FromJsonConfig(string path)
		{
			var config = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<JsonConfig>(config)!;
		}
	}
}