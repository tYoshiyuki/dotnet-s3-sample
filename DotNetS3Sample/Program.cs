
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Options;

namespace DotNetS3Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IAmazonS3>(provider =>
                {
                    var settings  = provider.GetRequiredService<IOptions<S3Settings>>().Value;
                    var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
                    return new AmazonS3Client(credentials, new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.APNortheast1,
                        ServiceURL = settings.ServiceURL
                    });
                });
            }
            else
            {
                // NOTE IAM Roleにてアクセスする想定のため、クレデンシャルは明示的に設定しない
                builder.Services.AddScoped<IAmazonS3>(_ => new AmazonS3Client(new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.APNortheast1,
                }));
            }


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
