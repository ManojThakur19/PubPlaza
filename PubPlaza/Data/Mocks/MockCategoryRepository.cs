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
        public IEnumerable<Category> Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
