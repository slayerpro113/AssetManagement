using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        Enumerations.AddEntityStatus HandleCreateOrder(string poRequestIdString, int staffCreateId);
        IList<Order> GetOrders();
    }
}
