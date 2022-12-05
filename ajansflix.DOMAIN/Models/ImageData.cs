using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("imagedata")]
    public class ImageData
    {
        public ImageData() => Id = Guid.NewGuid();

        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public string OriginalType { get; set; }
        public byte[] OriginalContent { get; set; }
        public byte[] ThumbnailContent { get; set; }
        public byte[] FullscreenContent { get; set; }
    }
}
