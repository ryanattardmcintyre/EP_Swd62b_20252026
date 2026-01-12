using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrdersRepository: IOrdersRepository
    {
        private ShoppingCartDbContext _context;
        public OrdersRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        private void AddOrderItem(OrderItem orderItem)
        { 
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }
        private void AddOrder(Order order)
        { 
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Checkout(Order order, List<OrderItem> orderItems, IBooksRepository booksRepo)
        {
            order.Id = Guid.NewGuid(); //i'm controlling the id of the Order
            AddOrder(order);
            foreach (OrderItem item in orderItems)
            {
                item.OrderFK = order.Id; //setting that generated id into each of the orderItems
                var book = booksRepo.Get(item.BookFK); //i'm getting the book from the database to check the stock
                if(book.Stock >= item.Qty) //checking the stock, if i have more than the qty being bought
                {
                    AddOrderItem(item); //add the order item into the database

                    book.Stock -= item.Qty;//updating the stock
                    booksRepo.Update(book);
                }
            }
        }

    }
}
