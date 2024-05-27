using Microsoft.AspNetCore.Identity;

namespace Ticketing.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<OrganizationUser>? OrganizationUsers { get; set; }
        public ICollection<SubTicket>? SubTickets { get; set; }
    }

}
