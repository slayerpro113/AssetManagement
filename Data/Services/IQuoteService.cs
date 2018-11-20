using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;
using System.Web;

namespace Data.Services
{
    public interface IQuoteService : IBaseService<Quote>
    {
        Enumerations.AddEntityStatus HandleQuote(Quote quote, PoRequest poRequest);
        Enumerations.UpdateEntityStatus EditQuote(Quote quote);
        IList<Quote> GetQuotesByPoRequestId(int poRequestId);
    }
}