using Domain.Models;

namespace Presentation.Models
{
    //ViewModels should be used only when there is the UI involved
    //whilst Models should be used to model the database (for migrations)
    public class BooksCreateViewModel
    {
        //The CLR uses a default constructor to instantiate an object of this type
        //when rendering the View
        public BooksCreateViewModel() { 
        }
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
    }
}
