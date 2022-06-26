openssl req -newkey rsa:2048 -x509 -nodes -keyout certs/azuriteKey.pem -new -out certs/azuriteCert.crt -sha256 -days 365 -addext "subjectAltName=IP:127.0.0.1,DNS:localazurite.blob.core.windows.net,DNS:localhost" -subj "/C=CO/ST=ST/L=LO/O=OR/OU=OU/CN=CN"

Import-Certificate -FilePath certs/azuriteCert.crt -CertStoreLocation Cert:\LocalMachine\TrustedPublisher