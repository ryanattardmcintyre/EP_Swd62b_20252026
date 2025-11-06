using DataAccess.Repositories;
using DataAccess.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class OrdersController : Controller
    {
        private OrdersRepository _ordersRepository;
        private BooksRepository _booksRepository;
        private ICalculatingTotal _calculatingService;

        //Constructor injection: injecting application service called OrdersRepository
        public OrdersController(OrdersRepository ordersRepository, BooksRepository booksRepository, ICalculatingTotal calculationService) {
        
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
            _calculatingService = calculationService;
        }

        public IActionResult Buy(List<OrderItem> orderItems)
        {
            Order order = new Order();
            order.DatePlaced = DateTime.Now;

            //BooksRepository booksRepository = new BooksRepository(); //bad practice

            _ordersRepository.Checkout(order, orderItems, _booksRepository);
            double finalTotal = _calculatingService.Calculate(orderItems);


            TempData["success"] = $"Final Total withdrawn is {finalTotal}";

            return RedirectToAction("Index", "Books"); //this is how to redirect to an action INSIDE ANOTHER CONTROLLER
        }
    }
}
