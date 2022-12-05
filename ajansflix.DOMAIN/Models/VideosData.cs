using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    public class VideosData : BaseEntity
    {
        public VideosData()
        {

        }
        public string VideoPath { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        public Products product { get; set; }
    }
}
