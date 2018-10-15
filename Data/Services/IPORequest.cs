using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IPORequest : IBaseService<PORequest>
    {
        Enumerations.AddEntityStatus HandlePoRequest(int productId, int employeeId, string description);
    }
}