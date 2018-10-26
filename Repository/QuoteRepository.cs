using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class QuoteRepository
    {
        public static int CountQuote(this IRepository<Quote> repository)
        {
            return repository.Entity.Count();
        }
    }
}
