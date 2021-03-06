﻿using Data.Entities;
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

        public static IList<PoRequest> GetPoRequestsFromUsers(this IRepository<PoRequest> repository)
        {
            return repository.Entity.Where(_ => _.RequestStatusID != 5).OrderByDescending(_ => _.PoRequestID).ToList();
        }

        public static IList<PoRequest> GetPoRequestsFromStaff(this IRepository<PoRequest> repository)
        {
            return repository.Entity.Where(_ => _.RequestStatusID == 2).OrderByDescending(_ => _.PoRequestID).ToList();
        }

        public static PoRequest GetUnfinishedPoRequestsByEmployeeId(this IRepository<PoRequest> repository, int employeeId, string categoryName )
        {
            return repository.Entity.FirstOrDefault(_ => _.EmployeeID == employeeId && _.CategoryName == categoryName && _.RequestStatusID != 5);
        }
    }
}