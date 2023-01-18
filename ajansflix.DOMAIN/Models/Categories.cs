using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("categories")]
    public class Categories : BaseEntity
    {
        public Categories()
        {
            products = new List<Products>();
        }

        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryStatus { get; set; }
        public string StatusCode { get; set; }
        public bool IsCampaign { get; set; }
        public int CategorySorted { get; set; }
        public virtual ICollection<Products> products { get; set; } 
        
    }
}
