using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;

namespace Service
{
    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWork unitOfWork, IRepository<OrderDetail> orderDetailRepository) : base(unitOfWork, orderDetailRepository)
        {
        }
    }
}