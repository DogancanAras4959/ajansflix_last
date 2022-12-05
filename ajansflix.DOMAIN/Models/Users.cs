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
    [Table("users")]
    public class Users : BaseEntity
    {
        public Users()
        {

        }

        [MaxLength(50)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }


        [ForeignKey("role")]
        public int RoleId { get; set; }
        public Roles role { get; set; }

    }
}
