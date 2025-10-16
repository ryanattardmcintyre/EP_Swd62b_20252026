using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using Domain.Models;
namespace DataAccess.Repositories
{
    //Repository classes will serve as raw CRUD methods to the database
    //C = create
    //R = read
    //U = update
    //D = delete

    //IQueryable vs IEnumerable/List<...>
    //IQueryable = it doesn't execute the command - it prepares an SQL command
    //IEnumerable = every command is executed i.e. it opens a connection

    public class BooksRepository
    {
        //This is called constructor injection
        private ShoppingCartDbContext _context;
        public BooksRepository(ShoppingCartDbContext context) {
            _context = context;
        }
        public IQueryable<Book> Get()
        { 
           return _context.Books;
        }

        public IQueryable<Book> Get(string keyword)
        {
            return Get().Where(x=>x.Title.Contains(keyword));
        }

        public Book Get(int id)
        {
            return Get().SingleOrDefault(x => x.Id == id);
        }

        public void Add(Book book) { 
            _context.Books.Add(book);
            _context.SaveChanges(); 
        }
        public void Update(Book book) {
            var original = Get(book.Id);
            if(original != null)
            {
                original.Title = book.Title;
                original.CategoryFK = book.CategoryFK;
                original.PublishedYear = book.PublishedYear;
                original.Stock = book.Stock;
                original.WholesalePrice = book.WholesalePrice;

                _context.SaveChanges(); //persist everything into the database
            }
        
        }
        public void Delete(int id)
        {
            var original = Get(id);
            if (original != null)
            {
                _context.Books.Remove(original);
                _context.SaveChanges();
            }
        }
 
    }
}
