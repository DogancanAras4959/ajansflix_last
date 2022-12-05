using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ImageData
{
    public class ImageDataDto
    {
        public ImageDataDto() => Id = Guid.NewGuid();

        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public string OriginalType { get; set; }
        public byte[] OriginalContent { get; set; }
        public byte[] ThumbnailContent { get; set; }
        public byte[] FullscreenContent { get; set; }
    }
}
