using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PComment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
        public DateTime CommentTime { get; set; }
        public string Content { get; set; }
    }
}
