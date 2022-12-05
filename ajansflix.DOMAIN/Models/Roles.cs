using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("roles")]
    public class Roles : BaseEntity
    {
        public Roles()
        {
            userRoles = new List<Users>();
        }

        [MaxLength(50)]
        [Required]
        public string roleName { get; set; }

        public ICollection<Users> userRoles { get; set; }
    }
}
