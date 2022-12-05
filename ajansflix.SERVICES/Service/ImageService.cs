using ajansflix.DOMAIN;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Dtos.ImageData;
using ajansflix.SERVICES.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class ImageService : IImageService
    {
        private const int ThumbnailWdith = 300;
        private const int FullScreenWidth = 1000;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ajansflixdbcontext _data;

        public ImageService(ajansflixdbcontext data, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _data = data;
        }

        public Task<List<string>> GetAllImages() => _data.imagefile.Select(i => i.Folder + "/thumbnail_" + i.Id + ".jpg").ToListAsync();

        public Task<List<string>> GetAllImagesByProductId(int ProductId) => _data.imageProductFiles.Where(x=> x.ProductId == ProductId).Select(i => i.Folder + "/thumbnail_" + i.Id + ".jpg").ToListAsync();

        public Task<byte[]> GetFullScreen(string id) => _data.imagedatas.Where(x => x.Id.ToString() == id).Select(x => x.FullscreenContent).FirstOrDefaultAsync();

        public int GetImageFromProduct(string path, int productId)
        {
            try
            {
                string lastPath = path.Replace(".jpg", "").Substring(1).ToLower();
                var folderPath = _data.imageProductFiles.Where(x => x.ProductId == productId && x.Id.ToString() == lastPath.Trim()).FirstOrDefault();
                 _data.imageProductFiles.Remove(folderPath);
                _data.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
           
        }

        public Task<byte[]> GetThumbnail(string id) => _data.imagedatas.Where(x => x.Id.ToString() == id).Select(x => x.ThumbnailContent).FirstOrDefaultAsync();

        public async Task Process(IEnumerable<ImageInputModel> images)
        {
            var totalImages = await _data.imagefile.CountAsync();

            var task = images.Select(image => Task.Run(async () =>
            {
                try
                {
                    using var imageResult = await Image.LoadAsync(image.Content);

                    var id = Guid.NewGuid();
                    var path = $"/images/{totalImages % 1000}/";
                    var name = $"{id}.jpg";

                    var storage = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                    if (!Directory.Exists(storage))
                    {
                        Directory.CreateDirectory(storage);
                    }

                    await SaveImage(imageResult, $"original_{name}", storage, imageResult.Width);
                    await SaveImage(imageResult, $"fullscreen_{name}", storage, FullScreenWidth);
                    await SaveImage(imageResult, $"thumbnail_{name}", storage, ThumbnailWdith);

                    var data = _serviceScopeFactory
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<ajansflixdbcontext>();

                    data.imagefile.Add(new ImageFile
                    {
                        Id = id,
                        Folder = path
                    });

                    await data.SaveChangesAsync();
                }
                catch
                {

                }
            })).ToList();

            await Task.WhenAll(task);
        }
        public async Task<string> ProcessFile(ImageInputModel images)
        {
            var totalImages = await _data.imagefile.CountAsync();

            try
            {
                using var imageResult = await Image.LoadAsync(images.Content);

                var id = Guid.NewGuid();
                var path = $"/images/{totalImages % 1000}/";
                var name = $"{id}.jpg";

                var storage = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                if (!Directory.Exists(storage))
                {
                    Directory.CreateDirectory(storage);
                }

                await SaveImage(imageResult, $"original_{name}", storage, imageResult.Width);
                await SaveImage(imageResult, $"fullscreen_{name}", storage, FullScreenWidth);
                await SaveImage(imageResult, $"thumbnail_{name}", storage, ThumbnailWdith);

                var data = _serviceScopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ajansflixdbcontext>();

                data.imagefile.Add(new ImageFile
                {
                    Id = id,
                    Folder = path
                });

                await data.SaveChangesAsync();

                return path + $"thumbnail_{name}";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task ProcessProductImages(IEnumerable<ImageInputModel> images, int ProductId)
        {
            var totalImages = await _data.imageProductFiles.CountAsync();

            var task = images.Select(image => Task.Run(async () =>
            {
                try
                {
                    using var imageResult = await Image.LoadAsync(image.Content);

                    var id = Guid.NewGuid();
                    var path = $"/images/{totalImages % 1000}/";
                    var name = $"{id}.jpg";

                    var storage = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                    if (!Directory.Exists(storage))
                    {
                        Directory.CreateDirectory(storage);
                    }

                    await SaveImage(imageResult, $"original_{name}", storage, imageResult.Width);
                    await SaveImage(imageResult, $"fullscreen_{name}", storage, FullScreenWidth);
                    await SaveImage(imageResult, $"thumbnail_{name}", storage, ThumbnailWdith);

                    var data = _serviceScopeFactory
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<ajansflixdbcontext>();

                    data.imageProductFiles.Add(new ProductImages
                    {
                        Id = id,
                        Folder = path,
                        ProductId = ProductId,
                    });

                    await data.SaveChangesAsync();
                }
                catch
                {

                }
            })).ToList();

            await Task.WhenAll(task);
        }
        private async Task SaveImage(Image image, string name, string path, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image.Mutate(i => i.Resize(new Size(width, height)));
            image.Metadata.ExifProfile = null;

            await image.SaveAsJpegAsync($"{path}/{name}", new JpegEncoder
            {
                Quality = 75,
            });

        }

    }
}
