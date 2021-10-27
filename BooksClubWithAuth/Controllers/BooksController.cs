using BooksClubWithAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace BooksClubWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        ApplicationContext db;
        public BooksController( ApplicationContext context)
        {
            db = context;
            if(!db.Books.Any())
            {
                db.Add(new Book { Id = 1, Name = "Одиссея капитана Блада.", Autor = "Рафаэль Сабатини",
                    Annotation = "Бакалавр медицины Питер Блад, обвиненный в государственной измене — за то," +
                                " что, верный клятве Гиппократа, оказал помощь раненому мятежнику, — приговорен" +
                                " к каторжным работам в южных колониях Великобритании. Спустя полгода, совершив" +
                                " дерзкий побег с острова Барбадос на захваченном испанском галеоне, он начинает новую," +
                                " полную приключений и опасностей жизнь капитана пиратского корабля и вскоре становится" +
                                " легендой берегового братства и грозой Карибского моря. Благородный разбойник," +
                                " волей судьбы оказавшийся вне закона, но не утративший понятий о добре, чести и справедливости."
                });
                db.Add(new Book { Id = 2, Name = "Алиса в Зазеркалье.", Autor = "Льюис Кэрролл",
                    Annotation = "Продолжение книги Льюиса Кэрролла Алиса в Стране Чудес. На этот раз героиня" +
                                " отправится в мир Зазеркалья, где становится пешкой на сказочной шахматной доске." +
                                " Читателей ждет множество удивительных приключений с участием фантастических зазеркальных героев." });
                db.Add(new Book
                {
                    Id = 3,
                    Name = "Мастер и Маргарита",
                    Autor = "Михаил Булгаков",
                    Annotation = "Феерическая сатира на быт и нравы Москвы 30-х годов, одновременно пронзительная история любви" +
                    " Мастера и Маргариты и вечная библейская тема борьбы добра со злом"
                });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            Ok(db.Books);
            return db.Books.ToList();
        }
        [HttpGet("id")]
        public Book GetBook(int id)
        {
                Book book = db.Books.FirstOrDefault(b=> b.Id == id);
                return book;
        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if(ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return Ok(book);
            }
            return BadRequest(ModelState);
        }
    }
}
