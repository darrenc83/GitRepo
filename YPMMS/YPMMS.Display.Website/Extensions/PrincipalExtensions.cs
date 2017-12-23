using System;
using System.Linq;
using System.Security.Principal;
using YPMMS.Shared.Core.Enums;

namespace YPMMS.Display.Website.Extensions
{
    /// <summary>
    /// Extensions on the <see cref="IPrincipal"/> interface.
    /// (Not called IPrincipalExtensions because then it looks like an interface itself.)
    /// </summary>
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Find out if a user has a specific role
        /// </summary>
        public static bool HasRole(this IPrincipal user, Role role)
        {
            return user.IsInRole(role.ToString());
        }

        /// <summary>
        /// Get the role assigned to a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Role GetRole(this IPrincipal user)
        {
            foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>())
            {
                if (user.HasRole(role))
                {
                    return role;
                }
            }

            throw new Exception($"Cannot determine role for user {user.Identity.Name}");
        }
    }
}