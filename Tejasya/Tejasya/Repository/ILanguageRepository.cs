using System.Collections.Generic;
using System.Threading.Tasks;
using Tejasya.Models;

namespace Tejasya.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}