using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Service
{
    public class FileService(IAmazonS3 _s3)
    {
        private readonly string _bucketName = "tkfilebucket1";

        public async Task<PutObjectResponse> UploadFile(Guid id, IFormFile file, string fileDirectory)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = $"{fileDirectory}/{id}",
                ContentType = file.ContentType,
                InputStream = file.OpenReadStream(),
                Metadata =
                {
                ["x-amz-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
                }
            };

            return await _s3.PutObjectAsync(putObjectRequest);
        }

        public async Task<GetObjectResponse> GetFile(Guid id,string fileDirectory)
        {
            var getObjectRequest = new GetObjectRequest { BucketName = _bucketName, Key = $"{fileDirectory}/{id}" };
            return await _s3.GetObjectAsync(getObjectRequest);
        }

        public async Task<DeleteObjectResponse> DeleteFile(Guid id,string fileDirectory)
        {
            var deleteObjectRequest = new DeleteObjectRequest { BucketName = _bucketName, Key = $"{fileDirectory}/{id}" };
            return await _s3.DeleteObjectAsync(deleteObjectRequest);
        }

    }
}
