using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Core
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = new int();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
