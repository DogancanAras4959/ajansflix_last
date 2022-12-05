using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ajansflix.Areas.admin.Data.AuthorizationHandler
{
    public static class AuthorizationPolicyBuilderExtensionHandler
    {
        public static AuthorizationPolicyBuilder UserRequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomUserRequireClaim(claimType));

            return builder;
        }

    }
}
