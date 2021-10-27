using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BooksClubWithAuth.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public Guid AccountsId { get; set; }        
        public List<Book> Books { get; set; }
        public DateTime DateTime { get; set; }
    }
}
