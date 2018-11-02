using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using Repository;

namespace Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IRepository<PoRequest> _poRequestRepository;

        private readonly IRepository<Product> _productRepository;

        public OrderService(IUnitOfWork unitOfWork, IRepository<Order> orderRepository, IRepository<PoRequest> poRequestRepository, IRepository<Product> productRepository) : base(unitOfWork, orderRepository)

        {
            _poRequestRepository = poRequestRepository;
            _productRepository = productRepository;
        }

        public Enumerations.AddEntityStatus HandleCreateOrder(string poRequestIdString)
        {
            try
            {
                var order = new Order();
                IList<PoRequest> poRequests = new List<PoRequest>();
                var poRequestIdArray = poRequestIdString.Split(',');
                decimal orderTotal = 0;

                foreach (var poRequestId in poRequestIdArray)
                {
                    var poReQuest = _poRequestRepository.GetEntity(int.Parse(poRequestId));
                    orderTotal += poReQuest.Quotes.FirstOrDefault(_ => _.QuoteID == poReQuest.SelectedQuoteID).Price;

                    poRequests.Add(poReQuest);
                }

                order.PoRequests = poRequests;
                order.PurchaseDate = DateTime.Now.Date;
                order.OrderTotal = orderTotal;

                IList<OrderDetail> orderDetails = new List<OrderDetail>();

                var quotes = GetQuotesByPoRequests(poRequests);

                foreach (var quote in quotes)
                {
                    var orderDetail = new OrderDetail();

                    if (orderDetails.Count == 0)
                    {
                        orderDetail.Price = quote.Price;
                        orderDetail.Quantity = 1;
                        orderDetail.Subtotal = orderDetail.Price * orderDetail.Quantity;
                    }
                    else
                    {
                        foreach (var tempOrderDetail in orderDetails)
                        {
                            //if(quote.ProductName == tempOrderDetail.)
                        }
                    }
                    orderDetails.Add(orderDetail);
                }

                foreach (var quote in quotes)
                {
                    //check if product exist
                    var count = _productRepository.CountProductByName(quote.ProductName);
                    if (count == 0)
                    {
                        var product = new Product();
                        product.ProductName = quote.ProductName;
                        product.Brand = quote.Brand;
                        product.CategoryID = GetVendorIdByVendorName(quote.CategoryName);
                    }
                    //save product to db

                    //handle asset
                    var productExist = _productRepository.GetProductsByCategoryName(quote.CategoryName);

                        var asset = new Asset();
                        asset.Product = productExist;
                        asset.Warranty = quote.Warranty;
                        asset.AssetStatusID = 1;
                    
                }

                AddEntity(order);
                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }

        public IList<Quote> GetQuotesByPoRequests(IList<PoRequest> poRequests)
        {
            IList<Quote> quotes = new List<Quote>();

            foreach (var poRequest in poRequests)
            {
                var quote = poRequest.Quotes.FirstOrDefault(_ => _.QuoteID == poRequest.SelectedQuoteID);
                quotes.Add(quote);
            }

            return quotes;
        }

        public int GetVendorIdByVendorName(string vendorName)
        {
            if (vendorName == "Hoa Phat")
            {
                return 1;
            }
            else if (vendorName == "Phi Long")
            {
                return 2;
            }
            else if (vendorName == "Phong Vu")
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}