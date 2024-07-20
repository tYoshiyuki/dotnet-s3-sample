# dotnet-s3-sample
AWS SDK for .NET と ASP.NET Core で S3バケットの情報を取得するサンプル

## Feature
- .NET8
- ASP.NET Core
- AWS SDK for .NET

## Note
- ローカル開発の際は、minio (S3のエミュレータ) が必要です。

```
docker run -p 9000:9000 -p 9001:9001 minio/minio server /data --console-address ":9001"
```
