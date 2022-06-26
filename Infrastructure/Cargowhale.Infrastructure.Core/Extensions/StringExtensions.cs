using System.Security;

namespace Cargowhale.Infrastructure.Core.Extensions
{
	public static class StringExtensions
	{
		public static unsafe SecureString ToSecureString(this string str)
		{
			fixed (char* fixedStringPointer = str)
			{
				return new SecureString(fixedStringPointer, str.Length);
			}
		}
	}
}