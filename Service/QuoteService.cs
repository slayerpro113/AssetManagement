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
        private readonly IRepository<Product> _productRepository;

        public QuoteService(IUnitOfWork unitOfWork, IRepository<Quote> quoteRepository, IRepository<Product> productRepository) : base(unitOfWork, quoteRepository)
        {
            _quoteRepository = quoteRepository;
            _productRepository = productRepository;
        }

        public Enumerations.AddEntityStatus HandleQuote(Quote quote, PoRequest poRequest)
        {
            try
            {
                if (IsExistedProduct(quote.ProductName))
                {
                    var product = _productRepository.GetProductByProductName(quote.ProductName);
                    quote.Image = product.Image;
                }
                else
                {
                    HandleImage(quote, quote.CategoryName);
                }

                quote.PoRequest = poRequest;
                AddEntity(quote);
                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }

        public bool IsExistedProduct(string productName)
        {
            var count = _productRepository.CountProductByName(productName);
            if (count != 0)
            {
                return true;
            }
            else
            {
                return false;
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

        public Quote HandleImage(Quote quote, string categoryName)
        {
            if (categoryName == "Chair")
            {
                quote.Image = "Chair/Chair1.jpg";
            }
            else if (categoryName == "Keyboard")
            {
                quote.Image = "Keyboard/Keyboard1.jpg";
            }
            else if(categoryName == "Mouse")
            {
                quote.Image = "Mouse/Mouse1.jpg";
            }
            else if(categoryName == "PC")
            {
                quote.Image = "PC/PC1.jpg";
            }
            else 
            {
                quote.Image = "Screen/Screen1.jpg";
            }

            return quote;
        }
    }
}