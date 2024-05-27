namespace Ticketing.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public required int SubTicketId { get; set; }
        public required string FilePath { get; set; }
        public SubTicket? SubTicket { get; set; }
    }

}
