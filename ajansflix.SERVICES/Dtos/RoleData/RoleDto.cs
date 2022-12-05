using ajansflix.SERVICES.Dtos.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.RoleData
{
    public class RoleDto : BaseEntityDto
    {
        public string roleName { get; set; }

        public ICollection<UserDto> userRoles { get; set; }
    }
}
