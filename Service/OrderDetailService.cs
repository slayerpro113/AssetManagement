using System.Collections.Generic;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;

namespace Service
{
    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        public OrderDetailService(IUnitOfWork unitOfWork, IRepository<OrderDetail> orderDetailRepository) : base(unitOfWork, orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public IList<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            return _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
        }
    }
}