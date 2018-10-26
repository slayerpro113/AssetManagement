using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<Asset> _assetRepository;

        public PoRequestService(IUnitOfWork unitOfWork, IRepository<PoRequest> poRequestRepository, IRepository<Asset> assetRepository) : base(unitOfWork, poRequestRepository)
        {
            _poRequestRepository = poRequestRepository;
            _assetRepository = assetRepository;
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

        public IList<PoRequest> GetPoRequestsByEmployeeId(int employeeId)
        {
            return _poRequestRepository.GetPoRequestsByEmployeeId(employeeId);
        }

        public IList<PoRequest> GetPoRequests()
        {
            var poRequests = GetAll().OrderByDescending(_ => _.PoRequestID);
            var requestsAvailableAsset = new List<PoRequest>();
            foreach (var poRequest in poRequests)
            {
                var quantity = _assetRepository.GetQuantityOfAsset(poRequest.CategoryName);
                if (poRequest.RequestStatusID != 3)
                {
                    poRequest.Quantity = quantity;
                    requestsAvailableAsset.Add(poRequest);
                }             
            }

            return requestsAvailableAsset.OrderByDescending(_ => _.CreatedDate).ToList();
        }
    }
}