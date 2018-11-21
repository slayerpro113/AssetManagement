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
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IRepository<Order> orderRepository, IRepository<PoRequest> poRequestRepository,
            IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<OrderDetail> orderDetailRepository) : base(unitOfWork, orderRepository)

        {
            _poRequestRepository = poRequestRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public Enumerations.AddEntityStatus HandleCreateOrder(string poRequestIdString, int staffCreateId)
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

                order.PurchaseDate = DateTime.Now.Date;
                order.OrderTotal = orderTotal;

                IList<OrderDetail> orderDetails = new List<OrderDetail>();
                var quotes = GetQuotesByPoRequests(poRequests);

                foreach (var quote in quotes)
                {
                    //handle order detail
                    var orderDetail = new OrderDetail();

                    if (orderDetails.Count == 0)
                    {
                        orderDetail.Quote = quote;
                        orderDetail.Price = quote.Price;
                        orderDetail.Quantity = 1;
                        orderDetail.Subtotal = orderDetail.Price * orderDetail.Quantity;

                        orderDetails.Add(orderDetail);
                    }
                    else
                    {
                        var tempOrderDetail =
                            orderDetails.FirstOrDefault(_ => _.Quote.ProductName == quote.ProductName);
                        if (tempOrderDetail == null)
                        {
                            orderDetail.Quote = quote;
                            orderDetail.Price = quote.Price;
                            orderDetail.Quantity = 1;
                            orderDetail.Subtotal = orderDetail.Price * orderDetail.Quantity;

                            orderDetails.Add(orderDetail);
                        }
                        else
                        {
                            tempOrderDetail.Quantity = tempOrderDetail.Quantity + 1;
                            tempOrderDetail.Subtotal = tempOrderDetail.Price * tempOrderDetail.Quantity;
                            orderDetails.Add(tempOrderDetail);
                        }
                    }
                }

                // handle product and asset 
                foreach (var temp in orderDetails)
                {
                    temp.VendorID = GetVendorIdByVendorName(temp.Quote.Vendor);
                    //check if product not exist
                    var count = _productRepository.CountProductByName(temp.Quote.ProductName);
                    if (count == 0)
                    {
                        var product = new Product
                        {
                            Image = temp.Quote.Image,
                            ProductName = temp.Quote.ProductName,
                            Brand = temp.Quote.Brand,
                            Category = _categoryRepository.GetCategoryByCategoryName(temp.Quote.CategoryName)
                        };

                        IList<Asset> assets = new List<Asset>();
                        for (int i = 0; i < temp.Quantity; i++)
                        {
                            var asset = new Asset
                            {
                                Product = product,
                                Warranty = temp.Quote.Warranty,
                                AssetStatusID = 3
                            };
                            assets.Add(asset);
                        }

                        temp.Assets = assets;
                    }
                    else
                    {
                        //if exist
                        var productExist = _productRepository.GetProductByProductName(temp.Quote.ProductName);
                        //handle asset
                        IList<Asset> assets = new List<Asset>();
                        for (int i = 0; i < temp.Quantity; i++)
                        {
                            var asset = new Asset
                            {
                                Product = productExist,
                                Warranty = temp.Quote.Warranty,
                                AssetStatusID = 3
                            };
                            assets.Add(asset);
                        }

                        temp.Assets = assets;
                    }
                }
                order.OrderDetails = orderDetails;

                foreach (var tempPoRequest in poRequests)
                {
                    tempPoRequest.RequestStatusID = 4;
                }

                order.PoRequests = poRequests;
                order.EmployeeID = staffCreateId;
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