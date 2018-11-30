#Create Order
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
