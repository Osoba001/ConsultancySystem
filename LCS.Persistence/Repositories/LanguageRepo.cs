using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Repositories
{
    public class LanguageRepo : BaseRepo<LanguageTB>, ILanguageRepo
    {
        public LanguageRepo(LCSDbContext context) : base(context)
        {
        }

        public List<Language> Convertlist(List<LanguageTB> listTB)
        {
            var res=new List<Language>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }
    }
}
