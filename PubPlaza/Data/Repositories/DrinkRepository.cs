using PubPlaza.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubPlaza.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PubPlaza.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly PubPlazaContext _pubPlazaContext;

        public DrinkRepository(PubPlazaContext pubPlazaContext)
        {
            _pubPlazaContext = pubPlazaContext;
        }
        public IEnumerable<Drink> AllDrinks => _pubPlazaContext.Drinks.Include(c => c.Category);

        public IEnumerable<Drink> PrefferedDrinks => _pubPlazaContext.Drinks.Where(p => p.IsPrefferedDrink).Include(c => c.Category);

        public Drink GetSingleDrink(int DrinkId) => _pubPlazaContext.Drinks.FirstOrDefault(d => d.DrinkId == DrinkId);
    }
}
