namespace Ticketing.Models
{
    public class SubTicket
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public required string Message { get; set; }
        public required int MessageOrder { get; set; }
        public DateTime SendDateTime { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
    }

}
