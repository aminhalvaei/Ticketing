namespace Ticketing.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}

