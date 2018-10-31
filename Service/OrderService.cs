using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;

namespace Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IRepository<PoRequest> _poRequestRepository;
        public OrderService(IUnitOfWork unitOfWork, IRepository<Order> orderRepository, IRepository<PoRequest> poRequestRepository) : base(unitOfWork, orderRepository)
        {
            _poRequestRepository = poRequestRepository;
        }

        public Enumerations.AddEntityStatus HandleCreateOrder(string poRequestIdString)
        {
            try
            {
                var order = new Order();
                IList<PoRequest> poRequests = new List<PoRequest>();
                var array = poRequestIdString.Split(',');
                var lengthArray = array.Length;
                decimal  orderTotal = 0; 
                for (int i =0; i < lengthArray; i++)
                {
                    var a = int.Parse(array[i]);
                    var poReQuest = _poRequestRepository.GetEntity(a);
                    orderTotal += poReQuest.Quotes.FirstOrDefault(_ => _.QuoteID == poReQuest.SelectedQuoteID).Price;

                    poRequests.Add(poReQuest);
                }

                order.PoRequests = poRequests;
                order.PurchaseDate = DateTime.Now.Date;
                order.OrderTotal = orderTotal;

                AddEntity(order);
                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.AddEntityStatus.Failed;
            }

        }
    }
}
