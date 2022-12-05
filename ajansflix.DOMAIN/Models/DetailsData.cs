using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("detailsData")]
    public class DetailsData : BaseEntity, IEntity
    {
        public DetailsData()
        {

        }

        public string DataName { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsLock { get; set; }

        [ForeignKey("detail")]
        public int DetailId { get; set; }

        public Details detail { get; set; }
    }
}
