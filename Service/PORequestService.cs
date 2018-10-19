using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;

namespace Service
{
    public class PoRequestService : BaseService<PoRequest>, IPoRequestService
    {
        private readonly IRepository<PoRequest> _poRequestRepository;
        public PoRequestService(IUnitOfWork unitOfWork, IRepository<PoRequest> poRequestRepository) : base(unitOfWork, poRequestRepository)
        {
            _poRequestRepository = poRequestRepository;
        }

        public Enumerations.AddEntityStatus HandlePoRequest(int employeeId, string description, string device)
        {
            try
            {
                var poRequest = new PoRequest();
                if (!String.IsNullOrEmpty(description))
                {
                    poRequest.Description = description;
                }
                poRequest.EmployeeID = employeeId;
                poRequest.RequestStatusID = 1;
                DateTime today = DateTime.Now.Date;
                poRequest.CreatedDate = today;
                poRequest.CategoryName = device;

                AddEntity(poRequest);
                return Enumerations.AddEntityStatus.Success;
            }
            catch(Exception)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }

        public IList<PoRequest> GetPoRequestByEmployeeId(int employeeId)
        {
            var poRequests = _poRequestRepository.GetPoRequestByEmployeeId(employeeId);
            foreach (var poRquest in poRequests)
            {
                poRquest.CreatedDateString = poRquest.CreatedDate.ToShortDateString();
            }

            return poRequests;
        }
    }
}