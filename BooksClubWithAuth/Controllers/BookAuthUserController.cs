using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksClubWithAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace BooksClubWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthUserController : ControllerBase
    {
        public ApplicationContext db;
        public BookAuthUserController(ApplicationContext context)
        {
            db = context;
            if(!db.Favorites.Any())
            {

            }
        }
        private Guid UserId => Guid.Parse(User.Claims.Single(u => u.Type == ClaimTypes.NameIdentifier).Value); //разобрать что происходит
        [HttpGet]
        [Authorize(Roles ="User")]
        [Route("")]
        public IActionResult GetFavoritesBook()
        {
            if (!db.Favorites.Any(a => a.AccountsId == UserId))
                return Ok(Enumerable.Empty<Book>());
            var favoriteBooksIds = db.Favorites.Single(b => b.AccountsId == UserId).Books.AsEnumerable();          //Посмотреть
            var orderedBooks = db.Books.Where(b => b.Equals(favoriteBooksIds.First(a => a == b)));                               // переписать на более оптимальный
            return Ok(orderedBooks);
        }
    }
}
