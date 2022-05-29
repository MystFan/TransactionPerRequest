using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionPerRequest.Data.Entities;

internal class PostDbConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(10000);

        builder
            .HasOne(x => x.Blog)
            .WithMany(b => b.Posts)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
