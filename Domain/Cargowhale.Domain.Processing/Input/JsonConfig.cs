namespace Cargowhale.Domain.Processing.Input
{
	public class JsonConfig
	{
		public string Cron { get; set; } = default!;

		public InputArguments ExportOptions { get; set; } = default!;
	}
}