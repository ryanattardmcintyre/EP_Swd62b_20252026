using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Factory;

namespace Presentation.Controllers
{
    public class BulkImportController : Controller
    {
        public IActionResult BulkImport(string json,
            [FromKeyedServices("db")] IBooksRepository bookRepository, //keyed scoped service
            [FromServices] JournalsRepository journalsRepository //scoped service
            )
        {
            //call the factory class to build Journal/Book out of
            //the json passed

            //...so then they can be saved in to the db

            string jsonTestData = @"{ 
                  ""Title"": ""Clean Code"",
                  ""WholesalePrice"": 34.99,
                  ""PublishedYear"": 2008,
                  ""CategoryFK"": 1,
                  ""Stock"": 25,
                  ""Path"": ""images/books/cleancode.jpg""
                }";
            BookFactory f = new BookFactory();
            f.BuildAndSave(jsonTestData, bookRepository, journalsRepository);


            return View();
        }
    }
}
