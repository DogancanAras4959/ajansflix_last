using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("details")]
    public class Details : BaseEntity
    {
        public Details()
        {
            productDetails = new List<ProductDetails>();
            detailsDataList = new List<DetailsData>();
        }

        public string DetailName { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DetailsData> detailsDataList { get; set; }
        public virtual ICollection<ProductDetails> productDetails { get; set; }
    }
}
