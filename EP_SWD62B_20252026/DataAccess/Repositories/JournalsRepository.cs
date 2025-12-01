using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    //this will manage journals to/from db
    public class JournalsRepository
    {
        private ShoppingCartDbContext _context;
        public JournalsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void Add(Journal journal) { 
          _context.Journals.Add(journal);
            _context.SaveChanges();
        }
    }
}
