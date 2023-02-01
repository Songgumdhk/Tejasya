using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Data;
using Tejasya.Models;

namespace Tejasya.Repository
{


    //repositary name shoud be included in the statupclass
    //book controller ma repositary=_repositary ni thapna parxxa
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToListAsync();
        }
    }

}
