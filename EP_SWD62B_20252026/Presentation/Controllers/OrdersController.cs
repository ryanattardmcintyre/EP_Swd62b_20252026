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
        public OrdersController(OrdersRepository ordersRepository, BooksRepository booksRepository
            , ICalculatingTotal calculationService) {
        
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
            _calculatingService = calculationService;
        }

     
    }
}
