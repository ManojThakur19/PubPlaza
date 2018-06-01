using Microsoft.EntityFrameworkCore;
using PubPlaza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlaza.Data
{
    public class PubPlazaContext :DbContext
    {
        public PubPlazaContext(DbContextOptions<PubPlazaContext> options) :base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Drink> Drinks { get; set; }
    }
}
