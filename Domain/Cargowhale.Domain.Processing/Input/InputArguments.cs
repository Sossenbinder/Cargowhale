using Cargowhale.Contracts.DbExport.Database;
using CommandLine;

namespace Cargowhale.Domain.Processing.Input
{
	public class InputArguments
	{
		[Option('c', "configFile", HelpText = "Config file path in case you want to configure with a json file instead of cli arguments", SetName = "file")]
		public string? ConfigFileName { get; set; }

		[Option('b', "blobConnectionString", HelpText = "Connection string of the blob the data will be exported to", SetName = "manual")]
		public string BlobConnectionString { get; set; } = default!;

		[Option('d', "dbName", HelpText = "Database name which should be exported", SetName = "manual")]
		public string DatabaseName { get; set; } = default!;

		[Option('s', "sqlConnectionString", HelpText = "Sql connection string", SetName = "manual")]
		public string SqlConnectionString { get; set; } = default!;

		[Option('u', "dbUsername", HelpText = "Database user used for login", SetName = "manual")]
		public string DatabaseUsername { get; set; } = default!;

		[Option('p', "dbPassword", HelpText = "Database password used for login", SetName = "manual")]
		public string DatabasePassword { get; set; } = default!;

		[Option('i', "imageName", HelpText = "The name of the resulting image", SetName = "manual")]
		public string ImageName { get; set; } = default!;

		[Option("dockerUser", HelpText = "Docker username", SetName = "manual")]
		public string DockerUsername { get; set; } = default!;

		[Option("dockerPassword", HelpText = "Docker password", SetName = "manual")]
		public string DockerPassword { get; set; } = default!;

		[Option("tag", HelpText = "Image tag (optional)", SetName = "manual")]
		public string? Tag { get; set; }

		[Option("containerUrl", HelpText = "Container url to upload to", SetName = "manual")]
		public string ContainerUrl { get; set; } = default!;

		[Option("cron", HelpText = "Cron schedule for regeneration of backup")]
		public string Cron { get; set; } = default!;

		public DatabaseExportRequest ToExportRequest()
		{
			return new DatabaseExportRequest()
			{
				ContainerUrl = ContainerUrl,
				BlobConnectionString = BlobConnectionString,
				DatabaseName = DatabaseName,
				DockerUsername = DockerUsername,
				DockerPassword = DockerPassword,
				SqlConnectionString = SqlConnectionString,
				DatabasePassword = DatabasePassword,
				DatabaseUsername = DatabaseUsername,
				ImageName = ImageName,
				Tag = Tag,
			};
		}
	}
}