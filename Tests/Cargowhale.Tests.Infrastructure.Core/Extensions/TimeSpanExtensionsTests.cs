using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cargowhale.Infrastructure.Core.Extensions;

namespace Cargowhale.Tests.Infrastructure.Core.Extensions
{
	public class TimeSpanExtensionsTests
	{
		[Test]
		public async Task CustomAwaiterShouldNotStopTooSoon()
		{
			var timespan = TimeSpan.FromMilliseconds(100);

			var guidanceDelay = Task.Delay(50);
			var properDelay = timespan.GetAwaiter();

			await guidanceDelay;

			Assert.False(properDelay.IsCompleted);

			await Task.Delay(100);

			Assert.True(properDelay.IsCompleted);
		}


		[Test]
		public async Task CustomAwaiterShouldNotWaitTooLong()
		{
			var timespan = TimeSpan.FromMilliseconds(50);

			var guidanceDelay = Task.Delay(100);
			var properDelay = timespan.GetAwaiter();

			Assert.False(properDelay.IsCompleted);

			await guidanceDelay;

			Assert.True(properDelay.IsCompleted);
		}


	}
}
