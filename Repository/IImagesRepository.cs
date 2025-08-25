namespace BlogApplication.Repository
{
    public interface IImagesRepository
    {
        Task<string> UploadImage(IFormFile file);
    }
}
