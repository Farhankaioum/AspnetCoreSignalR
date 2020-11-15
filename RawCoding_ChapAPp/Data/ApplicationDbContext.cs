using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RawCoding_ChatApp.Models;

namespace RawCoding_ChapAPp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ChatUser>()
                .HasKey(k => new { k.UserId, k.ChatId});

            builder.Entity<ChatUser>()
                .HasOne(u => u.User)
                .WithMany(c => c.Chats)
                .HasForeignKey(u => u.UserId);

            builder.Entity<ChatUser>()
                .HasOne(c => c.Chat)
                .WithMany(u => u.Users)
                .HasForeignKey(c => c.ChatId);
                

            base.OnModelCreating(builder);
        }
    }
}
