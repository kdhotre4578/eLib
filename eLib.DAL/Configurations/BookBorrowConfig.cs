using eLib.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eLib.DAL.Configurations
{
    public class BookBorrowConfig : IEntityTypeConfiguration<BookBorrower>
    {
        public void Configure(EntityTypeBuilder<BookBorrower> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.UserId).IsRequired();
            builder.Property(m => m.BookId).IsRequired();
            builder.HasOne(m => m.BookBorrowed).WithOne().HasForeignKey<BookBorrower>(n => n.BookId);

            builder.ToTable("TblBooksBorrower");
        }
    }
}
