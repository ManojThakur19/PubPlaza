using PubPlaza.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubPlaza.Data.Models;

namespace PubPlaza.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PubPlazaContext _pubPlazaContext;

        public CategoryRepository(PubPlazaContext pubPlazaContext)
        {
            _pubPlazaContext = pubPlazaContext;
        }
        public IEnumerable<Category> Categories => _pubPlazaContext.Categories;
    }
}
