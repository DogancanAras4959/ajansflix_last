using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ajansflix.CORE.CartModels
{
    public class CartItem
    {
        public CartItem()
        {
            _compItems = new List<ComponentItem>();
        }


        public int Id { get; set; }
        public string Item { get; set; }
        public decimal BasePrice { get; set; }
        public string Image { get; set; }
        public List<ComponentItem> _compItems { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}", Item);

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

    }

    public class ComponentItem
    {
        public string CompName { get; set; }
        public string CompValue { get; set; }
        public decimal Price { get; set; }
        public int CompId { get; set; }
        public int ProductId { get; set; }
        public int BaseId { get; set; }
    }
}
