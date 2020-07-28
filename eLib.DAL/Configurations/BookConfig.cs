using Microsoft.EntityFrameworkCore;
using eLib.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eLib.DAL.Configurations
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Quantity);

            builder.ToTable("TblBooks");
        }
    }
}
