using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YPMMS.Display.Website.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The user's real name
        /// </summary>
        public string Name { get; set; }
        public long Account { get; set; }
 
        public DateTimeOffset LastViewedEventsTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}