using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class HistoryService : BaseService<History>, IHistoryService
    {
        private readonly IRepository<History> _historyRepository;

        public HistoryService(IUnitOfWork unitOfWork, IRepository<History> historyRepository) : base(unitOfWork, historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public IList<History> GetHistoriesByAssetId(int assetId)
        {
            var histories = _historyRepository.GetHistoriesByAssetId(assetId);
            return histories;
        }

        public Enumerations.AddEntityStatus HandleAssign(int assetId, int employeeId, string assignRemark, string staffAssign)
        {
            var history = new History();

            try
            {
                history.Asset.AssetStatusID = 2;
                DateTime today = DateTime.Now.Date;
                history.CheckinDate = today;
                history.EmployeeID = employeeId;
                history.AssetID = assetId;
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