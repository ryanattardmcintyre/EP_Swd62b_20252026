using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ActionFilters;
using Presentation.Models;

namespace Presentation.Controllers
{

    [Authorize]
    public class BooksController : Controller
    {
        //Contructor Injection is one of the variations of DEPENDENCY INJECTION

        private ILogger<BooksController> _logger;
        private BooksRepository _booksRepository { get; set; }
        private ICalculatingTotal _calculatingService { get; set; }
        private IOrdersRepository _ordersRepository { get; set; }

        //tightly coupled is when the data type for injection allows only 1 data type to be injected
        //loosely coupled is when the data type for injection allows for more than 1 data type to be injecte
        public BooksController([FromKeyedServices("db")] IBooksRepository booksRepository, ICalculatingTotal calculatingService
            , IOrdersRepository ordersRepository
            , ILogger<BooksController> logger
            
            ) {
         _booksRepository = (BooksRepository) booksRepository;
            _ordersRepository = ordersRepository;
            _calculatingService = calculatingService;

            _logger = logger;
        }

        
        [HttpGet]//loads a page with empty input controls
        public IActionResult Create([FromServices] CategoriesRepository categoriesRepository)
        {
            // BooksCreateViewModel
            //-> Book
            //-> List of categories
            BooksCreateViewModel myModel = new BooksCreateViewModel();
            myModel.Categories = categoriesRepository.Get().ToList(); //here a call to the db is done

            return View(myModel);
        }

        [HttpPost] //handles the submission form
        //Method Injection : use [FromServices] BooksRepository _booksRepository in the parameter list

        //Application Services: CategoriesRepository
        //Framework Services: IWebHostEnvironment
        public IActionResult Create(BooksCreateViewModel b, 
            [FromServices] CategoriesRepository categoriesRepository,
            [FromServices] IWebHostEnvironment host)
        {
            try
            {
                /* Trace
                 * Debug
                 * Error
                 * Information
                 * Warning
                 * Critical
                 * ...
                 */ 

                _logger.LogInformation("Entered the Create action");


                if(b.UpdatedFile != null)
                {
                    //we have a file
                    //generate a unique filename //3EDBEEA5-D8B4-42AD-9B70-E9C9D6DA1EBD
                    string uniqueFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(b.UpdatedFile.FileName);

                    _logger.LogInformation("filename generated: " + uniqueFilename);

                    //C:\Users\attar\source\repos\EP_Swd62b_20252026\EP_SWD62B_20252026\Presentation\wwwroot
                    //file needs to be saved
                    string absolutePath = host.WebRootPath + "/images/" + uniqueFilename;

                    _logger.LogWarning("About to start saving the file on server's disk...");

                    using (var mySavingStream = new FileStream(absolutePath, FileMode.CreateNew))
                    {
                        b.UpdatedFile.CopyTo(mySavingStream);
                    }

                    _logger.LogCritical("File "+uniqueFilename+" saved!!");

                    b.Book.Path = "/images/" + uniqueFilename;
                }

                _booksRepository.Add(b.Book);
                _logger.LogCritical("Details of "+ b.Book.Title+ "saved in the database");

                //Ways how you can pass data from the server side (i.e. controller) to the client side  (i.e. views)
                //TempData = it survives a redirection (if i redirect the user to Index page instead, TempData is still accessible)
                //ViewData or ViewBag = they are fine if there is no redirection and data is lost after you pass it to the page
                //                      ViewBag.success = "Book created successfully";
                //Session = in session whatever you store survives many redirections until the user logs out
                //          or until the user leaves the account unattended for x minutes
                //Cookies = cookies are files stored in the client browser - they may be used to store data BUT be careful - cookies are not encrypted
                //Models = so we can edit the Book class, add a property called Feedback and we set it with the data we want to pass back to the page

                TempData["success"] = "Book created successfully";
                return RedirectToAction("Create"); //<- loading back the View where the request came from with no book's data
            }
            catch (Exception ex)
            {
                b.Categories = categoriesRepository.Get().ToList();
                //log the error

                string bookidentification = "Id: " + b.Book.Id + " | Name: " + b.Book.Title;
                if (b.UpdatedFile != null) bookidentification += " | Uploaded file: " + b.UpdatedFile.FileName;
                 

                _logger.LogError(ex, "...while trying to create a book " + bookidentification);

                TempData["failure"] = "Error occurred. Book wasn't saved. Try again later we're working on it";
                return View(b); //<- loading back the View where the request came from with the submitted data

            }
      
        }


        public IActionResult Update(int id, [FromServices] CategoriesRepository categoriesRepository)
        {
            BooksCreateViewModel myModel = new BooksCreateViewModel();
            myModel.Categories = categoriesRepository.Get().ToList(); //here a call to the db is done
            myModel.Book = _booksRepository.Get(id);

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Update(BooksCreateViewModel b, [FromServices] CategoriesRepository categoriesRepository)
        {
            try
            {
                _booksRepository.Update(b.Book);
                TempData["success"] = "Book updated successfully";
                return RedirectToAction("Update", new { id = b.Book.Id}); 
            }
            catch (Exception ex)
            {
                b.Categories = categoriesRepository.Get().ToList();
                //log the error
                TempData["failure"] = "Error occurred. Book wasn't saved. Try again later we're working on it";
                return View(b); //<- loading back the View where the request came from with the submitted data

            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            //IQueryable >> prepares an sql statement
            //ToList<> or AsEnumerable() open a connection with the database
            //when you run the ToList() it also enables you inspect the data
            List<Book> list = _booksRepository.Get().ToList();
            return View(list); //a call will be initiated with the database
        
        }

        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof( FilterKeywordActionFilter)) ]
        public IActionResult Index(string keyword)
        {
            var filteredList = _booksRepository.Get(keyword);
            return View(filteredList);
        }


        [HttpGet] //optional only when there just one method named that way
        public IActionResult Details(int id)
        {
            var book = _booksRepository.Get(id);
            return View(book);
            
        }

        
        private IActionResult Delete(int[] ids)
        {
            try
            {
                foreach (int x in ids)
                {
                    _booksRepository.Delete(x);
                }
                TempData["success"] = "Book(s) deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["failure"] = "Books were not deleted. Try again";
                //log the message
            }

            return RedirectToAction("Index");
        }

        [Authorize] //which stops anyone who is not logged in from accessing the action
        public IActionResult Execute(int[] ids, int[]quantities, string todo)
        { 
            if(todo.ToLower() == "delete")
            {
                return Delete(ids);
            }
            else if(todo.ToLower() == "checkout")
            {
                List<OrderItem> items = new List<OrderItem>();
                for (int i = 0; i < ids.Length; i++)
                {
                    if (quantities[i] > 0)
                    {
                        items.Add(new OrderItem()
                        {
                            BookFK = ids[i],
                            Qty = quantities[i]
                        });
                    }
                }
               return Buy(items);
            }
            return RedirectToAction("Index");
        }

        private IActionResult Buy(List<OrderItem> orderItems)
        {
            Order order = new Order();
            order.DatePlaced = DateTime.Now;
            order.Username = User.Identity.Name; //this gives you the email address of the logged in user

            _ordersRepository.Checkout(order, orderItems, _booksRepository);
            double finalTotal = _calculatingService.Calculate(orderItems);

            TempData["success"] = $"Final Total withdrawn is {finalTotal}";

            return RedirectToAction("Index", "Books"); //this is how to redirect to an action INSIDE ANOTHER CONTROLLER
        }

        public IActionResult Test()
        {
            return View();
        }

    }
}
