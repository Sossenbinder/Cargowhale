namespace Cargowhale.Contracts.DbExport.Options
{
	public class ExportTimerOptions
	{
		public ManualTimerOptions? ManualTimerOptions { get; set; } = null!;

		public string? Cron { get; set; } = null!;
	}

	public record ManualTimerOptions
	{
		public int IntervalSeconds { get; set; }

		public DateTime? FirstOccurence { get; set; }
	}
}