using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        //Contructor Injection is one of the variations of DEPENDENCY INJECTION
        private BooksRepository _booksRepository;
        public BooksController(BooksRepository booksRepository) {
         _booksRepository = booksRepository;
        }

        [HttpGet]//loads a page with empty input controls
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //handles the submission form
        public IActionResult Create(Book b)
        {
            _booksRepository.Add(b);
            return View();
        }
         
    }
}
