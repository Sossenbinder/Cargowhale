using System.Runtime.CompilerServices;

namespace Cargowhale.Infrastructure.Core.Extensions
{
	public static class TimeSpanExtensions
	{
		public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
		{
			return Task.Delay(timeSpan).GetAwaiter();
		}
	}
}