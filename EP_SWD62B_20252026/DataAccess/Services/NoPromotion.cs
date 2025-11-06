using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class NoPromotion : ICalculatingTotal
    {
        private BooksRepository _booksRepository;
        public NoPromotion(BooksRepository booksRepository)
        { 
            _booksRepository = booksRepository;
        }
        public double Calculate(List<OrderItem> buyingItems)
        {
            double total = 0;
            foreach (var item in buyingItems)
            {
                var book = _booksRepository.Get(item.BookFK);
                total += book.WholesalePrice;
            }

            return total;
        }
    }
}
