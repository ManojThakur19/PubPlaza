using PubPlaza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlaza.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Drink> PrefferedDrinks { get; set; }
    }
}
