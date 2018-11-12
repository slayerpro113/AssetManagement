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

        public static IList<Quote> GetQuotesByPoRequestId(this IRepository<Quote> repository, int poRequestId)
        {
            return repository.Entity.Where(_ => _.PoRequestID == poRequestId).ToList();
        }
    }
}
