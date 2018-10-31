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
        private readonly IRepository<History> _historyRepository;

        public PoRequestService(IUnitOfWork unitOfWork, IRepository<PoRequest> poRequestRepository, IRepository<Asset> assetRepository, IRepository<History> historyRepository) : base(unitOfWork, poRequestRepository)
        {
            _poRequestRepository = poRequestRepository;
            _assetRepository = assetRepository;
            _historyRepository = historyRepository;
        }

        //user
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
            catch (Exception)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }

        public IList<PoRequest> GetPoRequestsByEmployeeId(int employeeId)
        {
            var poRequests = _poRequestRepository.GetPoRequestsByEmployeeId(employeeId);
            foreach (var poRequest in poRequests)
            {
                if (poRequest.RequestStatusID == 4)
                {
                    var history = _historyRepository.GetStaffAssignAndAssetImage(poRequest);
                    if (history != null)
                    {
                        poRequest.StaffAssign = history.StaffAssign;
                        poRequest.AssetImage = history.Asset.Product.Image;
                        poRequest.AssetName = history.Asset.Product.ProductName;
                    }
                }
            }

            return poRequests;
        }

        public IList<PoRequest> GetPoRequestsFromUsers()
        {
            var poRequests = _poRequestRepository.GetPoRequestsFromUsers();
            foreach (var poRequest in poRequests)
            {
                var quantity = _assetRepository.GetQuantityOfAsset(poRequest.CategoryName);
                poRequest.Quantity = quantity;
            }

            return poRequests;
        }

        public void SetStatusAndFinishDate(int poRequestId)
        {
            var poRequest = GetEntity(poRequestId);
            poRequest.RequestStatusID = 4;
            poRequest.FinishedDate = DateTime.Now.Date;
            UpdateEntity(poRequest);
        }

        public Enumerations.UpdateEntityStatus HandleSubmitRequest(int poRequestId, string staffSubmit)
        {
            try
            {
                var poRequest = GetEntity(poRequestId);
                poRequest.RequestStatusID = 2;
                poRequest.StaffSubmit = staffSubmit;

                UpdateEntity(poRequest);
                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
        }

        public IList<PoRequest> GetSubmittedPoRequests()
        {
            return _poRequestRepository.GetSubmittedPoRequests();
        }

        public IList<PoRequest> GetPoRequestsFromStaff()
        {
            return _poRequestRepository.GetPoRequestsFromStaff();
        }

        public IList<Quote> GetQuotesByPoRequestId(int poRequestId)
        {
            var poRequest = GetEntity(poRequestId);
            return poRequest.Quotes.ToList();
        }

        public bool IsExistQuoteId(int poRequestId, int quoteId)
        {
            var quotes = GetQuotesByPoRequestId(poRequestId);
            var quote = quotes.FirstOrDefault(_ => _.QuoteID == quoteId);
            if (quote != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Enumerations.UpdateEntityStatus HandleSelectQuote(int poRequestId, int quoteId)
        {
            try
            {
                var poRequest = GetEntity(poRequestId);
                poRequest.RequestStatusID = 3;
                poRequest.SelectedQuoteID = quoteId;

                UpdateEntity(poRequest);
                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
            
        }
    }
}