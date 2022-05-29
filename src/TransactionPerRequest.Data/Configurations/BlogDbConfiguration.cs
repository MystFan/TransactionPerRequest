using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionPerRequest.Data.Entities;

internal class BlogDbConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(2048);

        builder
            .HasMany(x => x.Posts)
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}