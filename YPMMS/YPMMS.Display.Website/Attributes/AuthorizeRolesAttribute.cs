using System.Web.Mvc;
using YPMMS.Shared.Core.Enums;

namespace YPMMS.Display.Website.Attributes
{
    /// <summary>
    /// Convenience derived class of <see cref="AuthorizeAttribute"/> so
    /// we can easily mark controllers and actions as accessible by <see cref="Role"/>.
    /// </summary>
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roles">One or more <see cref="Role"/> values</param>
        public AuthorizeRolesAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}