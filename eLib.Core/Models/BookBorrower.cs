using System;

namespace eLib.Core.Models
{
    public class BookBorrower
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public Book BookBorrowed { get; set; }
    }
}
