using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Web;

namespace Service
{
    public class QuoteService : BaseService<Quote>, IQuoteService
    {
        private readonly IRepository<Quote> _quoteRepository;

        public QuoteService(IUnitOfWork unitOfWork, IRepository<Quote> quoteRepository) : base(unitOfWork, quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public Enumerations.AddEntityStatus HandleQuote(HttpPostedFileBase image, Quote quote, PoRequest poRequest)
        {
            try
            {
                var quoteId = _quoteRepository.CountQuote() + 1;
                string imageName = quote.CategoryName + "/" + quote.CategoryName + quoteId  + ".jpg";
                //string imageName = System.IO.Path.GetFileName(image.FileName);
                string filePath = "~/Image/Categories/" + imageName;
                image.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                quote.Image = imageName;
                quote.PoRequest = poRequest;

                AddEntity(quote);
                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }
    }
}