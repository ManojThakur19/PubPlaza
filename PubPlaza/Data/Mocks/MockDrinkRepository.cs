using PubPlaza.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubPlaza.Data.Models;

namespace PubPlaza.Data.Mocks
{
    public class MockDrinkRepository : IDrinkRepository
    {
        public IEnumerable<Drink> AllDrinks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Drink> PrefferedDrinks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Drink GetSingleDrink(int DrinkId)
        {
            throw new NotImplementedException();
        }
    }
}
