using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class OrderDetailRepository
    {
        public static IList<OrderDetail> GetOrderDetailsByOrderId(this IRepository<OrderDetail> repository, int orderId)
        {
            return repository.Entity.Where(_ => _.OrderID == orderId).ToList();
        }
    }
}