using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class HistoryService : BaseService<History>, IHistoryService
    {
        private readonly IRepository<History> _historyRepository;
        private readonly IRepository<PoRequest> _poRequestRepository;
        private readonly IUnitOfWork _unitOfWork;


        public HistoryService(IUnitOfWork unitOfWork, IRepository<History> historyRepository, IRepository<PoRequest> poRequestRepository) : base(unitOfWork, historyRepository)
        {
            _unitOfWork = unitOfWork;
            _historyRepository = historyRepository;
            _poRequestRepository = poRequestRepository;
        }

        public IList<History> GetHistoriesByAssetId(int assetId)
        {
            var histories = _historyRepository.GetHistoriesByAssetId(assetId);
            return histories;
        }

        public Enumerations.AddEntityStatus HandleAssign(int poRequestId, Employee employee,Asset asset, string assignRemark, string staffAssign)
        {
            var poRequest = _poRequestRepository.GetEntity(poRequestId);
            try
            {
                _unitOfWork.BeginTransaction();

                poRequest.RequestStatusID = 4;
                poRequest.FinishedDate = DateTime.Now.Date;
                _poRequestRepository.UpdateEntity(poRequest);
                _unitOfWork.SaveChanges();


                var history = new History();
                
                history.Employee = employee;
                history.Asset = asset;
                history.Asset.AssetStatusID = 2;
                history.CheckinDate = DateTime.Now.Date;
                history.StaffAssign = staffAssign;
                if (!String.IsNullOrEmpty(assignRemark))
                {
                    history.AssignRemark = assignRemark;
                }

                AddEntity(history);
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();

                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }

            return Enumerations.AddEntityStatus.Failed;
        }

        public Enumerations.AddEntityStatus HandleAssignWithoutRequest(Employee employee, Asset asset, string assignRemark, string staffAssign)
        {
            try
            {
                var history = new History();

                history.Employee = employee;
                history.Asset = asset;
                history.Asset.AssetStatusID = 2;
                history.CheckinDate = DateTime.Now.Date;
                history.StaffAssign = staffAssign;
                if (!String.IsNullOrEmpty(assignRemark))
                {
                    history.AssignRemark = assignRemark;
                }

                AddEntity(history);

                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
        }

        public Enumerations.UpdateEntityStatus HandleRecall(int assetId, string remark, string staffRecall)
        {
            var history = _historyRepository.GetHistoryByAssetId(assetId);

            try
            {
                history.Asset.AssetStatusID = 1;
                DateTime today = DateTime.Now.Date;
                history.CheckoutDate = today;
                history.StaffRecall = staffRecall;
                if (!String.IsNullOrEmpty(remark))
                {
                    history.RecallRemark = remark;
                }

                UpdateEntity(history);

                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
        }

        public IList<History> GetHistoriesByEmployeeId(int employeeId)
        {
            return _historyRepository.GetHistoriesByEmployeeId(employeeId);
        }
    }
}