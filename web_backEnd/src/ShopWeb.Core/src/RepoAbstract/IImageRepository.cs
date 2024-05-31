using Microsoft.AspNetCore.Http;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IImageRepository
    {

        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public Task DeleteImage(string imageFileName);
    }
}