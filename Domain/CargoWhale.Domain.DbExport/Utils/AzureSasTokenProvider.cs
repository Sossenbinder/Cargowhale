using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace Cargowhale.Domain.Sql.Utils
{
	public static class AzureSasTokenProvider
	{
		public static string GetSasToken(string blobConnectionString)
		{
			var accountSasBuilder = new AccountSasBuilder();
			accountSasBuilder.SetPermissions(AccountSasPermissions.All);
			accountSasBuilder.Protocol = SasProtocol.Https;
			accountSasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddDays(1);
			accountSasBuilder.Services = AccountSasServices.Blobs;
			accountSasBuilder.ResourceTypes = AccountSasResourceTypes.Container | AccountSasResourceTypes.Object;

			var blobServiceClient = new BlobServiceClient(blobConnectionString);

			var sas = blobServiceClient.GenerateAccountSasUri(accountSasBuilder);

			return sas.Query[1..];
		}
	}
}