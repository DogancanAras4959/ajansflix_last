using ajansflix.SERVICES.Dtos.ImageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IImageService
    {
        Task Process(IEnumerable<ImageInputModel> images);

        Task ProcessProductImages(IEnumerable<ImageInputModel> images, int ProductId);
        Task<string> ProcessFile(ImageInputModel images);

        Task<byte[]> GetThumbnail(string id);
        Task<byte[]> GetFullScreen(string id);

        Task<List<string>> GetAllImages();

        int GetImageFromProduct(string path, int productId);

        Task<List<string>> GetAllImagesByProductId(int ProductId);
    }
}
