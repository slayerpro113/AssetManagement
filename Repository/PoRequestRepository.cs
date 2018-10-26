using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class PoRequestRepository
    {
        public static IList<PoRequest> GetPoRequestsByEmployeeId(this IRepository<PoRequest> repository, int employeeId)
        {
            return repository.Entity.Where(_ => _.EmployeeID == employeeId).OrderByDescending(_ => _.PoRequestID).ToList();
        }

        public static IList<PoRequest> GetPoRequests(this IRepository<PoRequest> repository)
        {
            return repository.Entity.Where(_ => _.RequestStatusID != 3).OrderByDescending(_ => _.PoRequestID).ToList();
        }
    }
}