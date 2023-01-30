using LCS.Domain.Entities;
using LCS.Domain.Models;

namespace LCS.Domain.Repositories
{
    public interface ILanguageRepo:IBaseRepo<LanguageTB>
    {
        List<Language> Convertlist(List<LanguageTB> listTB);
    }
}
