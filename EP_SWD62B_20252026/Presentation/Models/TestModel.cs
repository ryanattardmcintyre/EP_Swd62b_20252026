namespace Presentation.Models
{
    //role of models: are to carry data from point a to point b
    //                i.e. Point A = View to Point B = controller
    //                i.e. Point A = Controller to Point B = View
    public class TestModel
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }

    }
}
