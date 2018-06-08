using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PubPlaza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlaza.Data
{
    public class PubPlazaContext :IdentityDbContext<IdentityUser>
    {
        public PubPlazaContext(DbContextOptions<PubPlazaContext> options) :base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
