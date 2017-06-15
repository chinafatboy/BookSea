using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class BookType
    {
        public BookType()
        {
            BookClass = new HashSet<BookClass>();
        }
        public int Id { get; set; }
        public string ClassifType { get; set; }//粗略的分类例如文艺
        public string TyptName { get; set; }//细的分类名例如诗歌文学
        public virtual ICollection<BookClass> BookClass { get; set; }//ICollection是一个类似于List的接口
    }
}
