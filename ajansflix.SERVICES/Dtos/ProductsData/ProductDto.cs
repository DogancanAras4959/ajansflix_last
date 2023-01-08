using ajansflix.SERVICES.Dtos.CategoryData;
using ajansflix.SERVICES.Dtos.ProductDetailsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ProductsData
{
    public class ProductDto : BaseEntityDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductMetaName { get; set; }
        public string UrunNot { get; set; }
        public string ProductMetaDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductAlternateDesc { get; set; }
        public int Discount { get; set; }
        public string Banner { get; set; }
        public string ImagePath { get; set; }
        public int Carpan { get; set; }

        public int CategoryId { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}", ProductName);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public CategoryDto category { get; set; }

        public virtual ICollection<ProductDetailDto> productDetails { get; set; }
    }
}
