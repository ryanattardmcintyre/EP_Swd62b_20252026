using Castle.Core.Logging;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    //Decorator Design Pattern?

    //it will me to add logging instructrions to the older/original OrdersRepository without touching its
    //internal implementations

    //OrdersLoggingRepository => OrdersRepository + Logging
    public class OrdersLoggingRepository : IOrdersRepository
    {
        private readonly ILogger<OrdersLoggingRepository> _logger;
        private readonly IOrdersRepository _innerOrdersRepository;

          public OrdersLoggingRepository(ILogger<OrdersLoggingRepository> logger, 
              IOrdersRepository ordersRepository) {
            _logger = logger;
            _innerOrdersRepository = ordersRepository;
        }

        public void Checkout(Order order, List<OrderItem> orderItems, IBooksRepository booksRepo)
        {
            try
            {
                _logger.LogInformation("Checkout starting for these products: " + orderItems.Count);

                foreach (var item in orderItems)
                {
                    _logger.LogInformation($"Book Id: {item.BookFK} - Qty: {item.Qty}");
                }

                _innerOrdersRepository.Checkout(order, orderItems, booksRepo);

                _logger.LogInformation("Checkout done successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Checkout failed for books");
            }
        }
    }
}
