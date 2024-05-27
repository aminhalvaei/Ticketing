namespace Ticketing.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<OrganizationUser>? OrganizationUsers { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }

}
