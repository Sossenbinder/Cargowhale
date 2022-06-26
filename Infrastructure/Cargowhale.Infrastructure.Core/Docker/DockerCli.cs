using System.Diagnostics;
using Cargowhale.Infrastructure.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace Cargowhale.Infrastructure.Core.Docker
{
	public static class DockerCli
	{
		public static async Task<int> Run(ILogger logger, params string[] args)
		{
			var cts = new CancellationTokenSource();

			using var process = new Process()
			{
				StartInfo = new()
				{
					FileName = "docker",
					CreateNoWindow = true,
					RedirectStandardOutput = true,
				},
			};

			void OnExit(object? _, EventArgs __)
			{
				// ReSharper disable once AccessToDisposedClosure
				process.Exited -= OnExit;
				cts.Cancel();
			}

			process.EnableRaisingEvents = true;
			process.Exited += OnExit;
			process.ErrorDataReceived += (_, error) => logger.LogError(error.Data);
			process.OutputDataReceived += (_, data) => logger.LogInformation(data.Data);

			foreach (var arg in args)
			{
				process.StartInfo.ArgumentList.Add(arg);
			}

			process.Start();

			await RunOutput(cts.Token).IgnoreTaskCancelledException();
			return process.ExitCode;
		}

		private static async Task RunOutput(CancellationToken token)
		{
			Console.WriteLine("Running ");
			var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));

			while (await periodicTimer.WaitForNextTickAsync(token))
			{
				Console.Write(".");
			}
		}
	}
}