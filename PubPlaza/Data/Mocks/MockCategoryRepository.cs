using PubPlaza.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubPlaza.Data.Models;

namespace PubPlaza.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category {CategoryName="Alcoholic",Description="All Alcoholic Drinks"},
                    new Category {CategoryName="Non-Alcoholic",Description="All Non-Alcoholic Drinks"},

                };
            }
        }
    }
}
