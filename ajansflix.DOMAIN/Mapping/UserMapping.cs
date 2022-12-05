using ajansflix.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.role).WithMany(x => x.userRoles).HasForeignKey(x => x.RoleId);

        }
    }
}
