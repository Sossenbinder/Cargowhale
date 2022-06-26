namespace Cargowhale.Infrastructure.Core.Async
{
	public class AsyncDisposableFunction : IAsyncDisposable
	{
		private readonly Func<Task> _callback;

		public AsyncDisposableFunction(Func<Task> callback)
		{
			_callback = callback;
		}

		public async ValueTask DisposeAsync()
		{
			await _callback();
		}
	}
}