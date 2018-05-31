using PubPlaza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlaza.Data.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> AllDrinks { get; }
        IEnumerable<Drink> PrefferedDrinks { get; set; }
        Drink GetSingleDrink(int DrinkId);


    }
}
