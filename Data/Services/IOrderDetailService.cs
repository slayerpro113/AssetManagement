using Data.Entities;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IOrderDetailService : IBaseService<OrderDetail>
    {
        IList<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}