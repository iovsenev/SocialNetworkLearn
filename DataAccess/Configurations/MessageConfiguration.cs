using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e=> e.Recipient)
                .WithMany(e => e.IncomingMessages)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .UseIdentityColumn();
        }
    }
}
