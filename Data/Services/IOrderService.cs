using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        Enumerations.AddEntityStatus HandleCreateOrder(string poRequestIdString, int staffCreateId);
    }
}