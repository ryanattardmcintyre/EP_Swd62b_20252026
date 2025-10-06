using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            TestModel model = new TestModel();
            model.Author = User.Identity.IsAuthenticated ? User.Identity.Name : "Me";
            model.Message = "This is a test. Welcome to asp.net core site";
            model.DatePublished = DateTime.Now;

            return View(model);
        }

        public IActionResult SubmitForm(TestModel data)
        {
            //process
            if (string.IsNullOrEmpty(data.Message) == false)
            {
                int wordCount = data.Message.Split(new char[] { ' ' }).Length + 1;
                ViewBag.WordCount = wordCount;
            }

            data.Author = data.Author.ToUpper();

            
            //by default: redirect the user to a view called SubmitForm
            return View("Index", data); 

        }
    }
}
