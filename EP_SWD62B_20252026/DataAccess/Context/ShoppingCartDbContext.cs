using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    //The context class is an abstract representation of the database
    //Thus you must specify in here any tables which you would to create in your database
    //including any configurations which you want to be applied in the database e.g. auto-generation of GUID fields
    //This is the peak of CODE FIRST APPROACH

    //if we apply IdentityDbContext - it will automatically create tables (which the spec is hidden) that will
    //manage user accounts e.g. AspNetUsers, AspNetRoles, ....
    //Do you want to use User Accounts?
    public class ShoppingCartDbContext : IdentityDbContext //or DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        //to do: configure lazy loading and guid auto generation
    }
}
