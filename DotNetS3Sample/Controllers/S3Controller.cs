using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace DotNetS3Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IAmazonS3 s3;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="s3"></param>
        public S3Controller(IAmazonS3 s3)
        {
            this.s3 = s3;
        }

        /// <summary>
        /// バケットの一覧を取得します
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListBucket")]
        public async Task<IEnumerable<string>> ListBucket()
        {
            var response = await s3.ListBucketsAsync();
            return response.Buckets.Select(x => x.BucketName).ToList();
        }
    }
}
