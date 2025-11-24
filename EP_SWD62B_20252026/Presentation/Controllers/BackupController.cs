using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BackupController : Controller
    {
        //injecting 2 different implementations, inheriting from the same interface!
        public IActionResult Backup(
            [FromKeyedServices("db")] IBooksRepository bookDbRepo,
            [FromKeyedServices("file")] IBooksRepository bookFileRepo
            )
        {
            //read data from db
           var listOfBooksFromDb = bookDbRepo.Get().ToList();

           foreach(var book in listOfBooksFromDb)
            {
                //saving the read books from db into a json file
                bookFileRepo.Add(book);
            }
            return Content("Backup taken");
        }
    }
}
