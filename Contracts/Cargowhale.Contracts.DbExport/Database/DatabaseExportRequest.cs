using System.Reflection;

namespace Cargowhale.Contracts.DbExport.Database
{
	public class DatabaseExportRequest
	{
		public string BlobConnectionString { get; set; } = default!;

		public string DatabaseName { get; set; } = default!;

		public string SqlConnectionString { get; set; } = default!;

		public string DatabaseUsername { get; set; } = default!;

		public string DatabasePassword { get; set; } = default!;

		public string ImageName { get; set; } = default!;

		public string DockerUsername { get; set; } = default!;

		public string DockerPassword { get; set; } = default!;

		public string? Tag { get; set; }

		public string ContainerUrl { get; set; } = default!;

		public override string ToString()
		{
			var props = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

			return string.Join(',', props.Select(prop =>
			{
				var propValue = prop.GetValue(this);
				return $"{prop.Name}: {propValue}";
			}));
		}
	}
}