using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        //Contructor Injection is one of the variations of DEPENDENCY INJECTION
        private BooksRepository _booksRepository { get; set; }
        public BooksController(BooksRepository booksRepository) {
         _booksRepository = booksRepository;
        }

        [HttpGet]//loads a page with empty input controls
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //handles the submission form
        //Method Injection : use [FromServices] BooksRepository _booksRepository in the parameter list
        public IActionResult Create(Book b)
        {
            try
            {
                
                _booksRepository.Add(b);

                //Ways how you can pass data from the server side (i.e. controller) to the client side  (i.e. views)
                //TempData = it survives a redirection (if i redirect the user to Index page instead, TempData is still accessible)
                //ViewData or ViewBag = they are fine if there is no redirection and data is lost after you pass it to the page
                //                      ViewBag.success = "Book created successfully";
                //Session = in session whatever you store survives many redirections until the user logs out
                //          or until the user leaves the account unattended for x minutes
                //Cookies = cookies are files stored in the client browser - they may be used to store data BUT be careful - cookies are not encrypted
                //Models = so we can edit the Book class, add a property called Feedback and we set it with the data we want to pass back to the page

                TempData["success"] = "Book created successfully";
                return View(); //<- loading back the View where the request came from with no book's data
            }
            catch (Exception ex)
            {
                //log the error
                TempData["failure"] = "Error occurred. Book wasn't saved. Try again later we're working on it";
                return View(b); //<- loading back the View where the request came from with the submitted data

            }
      
        }


        /*public IActionResult Index()
        {
            var books = _booksRepository.Get();
        }

        public IActionResult Delete() {
            _booksRepository.Delete(1);
        }*/
    }
}
