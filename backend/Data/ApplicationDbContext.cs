
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<SubTicket> SubTickets { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships

        builder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(ou => new { ou.UserId, ou.OrganizationId });

            entity.HasOne(ou => ou.User)
                  .WithMany(u => u.OrganizationUsers)
                  .HasForeignKey(ou => ou.UserId);

            entity.HasOne(ou => ou.Organization)
                  .WithMany(o => o.OrganizationUsers)
                  .HasForeignKey(ou => ou.OrganizationId);
        });

        builder.Entity<Ticket>(entity =>
        {
            entity.HasMany(s => s.SubTickets).WithOne(a => a.Ticket);

            entity.HasKey(u => u.Id);

            entity.HasOne(u => u.Status)
                  .WithMany(s => s.Tickets)
                  .HasForeignKey(u => u.StatusId);

            entity.HasOne(u => u.Priority)
                  .WithMany(s => s.Tickets)
                  .HasForeignKey(u => u.PriorityId);

            entity.HasOne(u => u.Organization)
                  .WithMany(s => s.Tickets)
                  .HasForeignKey(u => u.OrganizationId);

        });

        builder.Entity<SubTicket>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasOne(u => u.User)
                  .WithMany(s => s.SubTickets)
                  .HasForeignKey(u => u.UserId);

            entity.HasOne(u => u.Ticket)
                  .WithMany(t => t.SubTickets)
                  .HasForeignKey(u => u.TicketId);

        });

        builder.Entity<Attachment>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasOne(u => u.SubTicket)
                  .WithMany(s => s.Attachments)
                  .HasForeignKey(u => u.SubTicketId);
        });


    }
}
