using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    public class BooksFileRepository : IBooksRepository
    {
        private string filePath = @"C:\\Users\\attar\\source\\repos\\EP_Swd62b_20252026\\EP_SWD62B_20252026\\Presentation\\Data\\books.json";
        public void Add(Book book)
        {
            string json = JsonConvert.SerializeObject(book);
            File.AppendAllLines(filePath, new List<string>() { json });
        }

        public IQueryable<Book> Get()
        {
            string [] fileContent  = File.ReadAllLines(filePath);
            List<Book> books = new List<Book>();
            foreach(string str in fileContent)
            {
                books.Add(JsonConvert.DeserializeObject<Book>(str));
            }

            return books.AsQueryable();
        }
    }
}
