using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IPoRequestService : IBaseService<PoRequest>
    {
        Enumerations.AddEntityStatus HandlePoRequest(int employeeId, string description, string device);
        IList<PoRequest> GetPoRequestsByEmployeeId(int employeeId);
        IList<PoRequest> GetPoRequestsFromUsers();
        Enumerations.UpdateEntityStatus HandleSubmitRequest(int poRequestId, int staffSubmitId);
        IList<PoRequest> GetPoRequestsFromStaff();
        bool IsExistQuoteId(int poRequestId, int quoteId);
        Enumerations.UpdateEntityStatus HandleSelectQuote(int poRequestId, int quoteId);

    }
}