
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogApplication.Repository
{
    public class CloudinaryImageRepository : IImagesRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account _account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
        }

        public IConfiguration Configuration { get; }

        public async Task<string> UploadImage(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = client.UploadAsync(uploadParams);

            if (uploadResult!=null&&uploadResult.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.Result.SecureUrl.ToString();
            }
            return null;
        }
    }
}
