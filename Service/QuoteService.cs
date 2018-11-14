using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                string imageName = quote.CategoryName + "/" + quote.ProductName + quoteId  + ".jpg";
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
      
        public IList<Quote> GetQuotesByPoRequestId(int poRequestId)
        {
            var quotes = _quoteRepository.GetQuotesByPoRequestId(poRequestId);

            foreach (var quote in quotes)
            {
                string path = HttpContext.Current.Server.MapPath("~/Image/Categories/" + quote.Image);
                quote.ImageBytes = File.ReadAllBytes(path);
            }

            return quotes;
        }

        public Enumerations.UpdateEntityStatus EditQuote(Quote editQuote)
        {
            try
            {
                var quote = GetEntity(editQuote.QuoteID);
                quote.ProductName = editQuote.ProductName;
                quote.Brand = editQuote.Brand;
                quote.Vendor = editQuote.Vendor;
                quote.Price = editQuote.Price;
                quote.Warranty = editQuote.Warranty;

                //handle image
                byte[] imageBytes = editQuote.ImageBytes;

                using (var ms = new MemoryStream(imageBytes))
                {
                    using (var image = Image.FromStream(ms))
                    {
                        image.Save(HttpContext.Current.Server.MapPath("~/Image/Categories/" + quote.Image), ImageFormat.Jpeg);
                    }
                }

                UpdateEntity(quote);

                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
        }
    }
}