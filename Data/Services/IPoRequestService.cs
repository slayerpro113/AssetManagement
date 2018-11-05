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
        void SetStatusAndFinishDate(int poRequestId);
        Enumerations.UpdateEntityStatus HandleSubmitRequest(int poRequestId, string staffSubmit);
        IList<PoRequest> GetPoRequestsFromStaff();
        IList<Quote> GetQuotesByPoRequestId(int poRequestId);
        bool IsExistQuoteId(int poRequestId, int quoteId);
        Enumerations.UpdateEntityStatus HandleSelectQuote(int poRequestId, int quoteId);

    }
}