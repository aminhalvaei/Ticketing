using System.Diagnostics.CodeAnalysis;

namespace Ticketing.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public int PriorityId { get; set; }
        public Priority? Priority { get; set; }
        public DateTime EndDateTime { get; set; }

        public ICollection<SubTicket>? SubTickets { get; set; }
    }
}

