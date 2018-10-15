using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using System;
using System.Collections.Generic;
using Data.Utilities.Enumeration;

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

        public Enumerations.AddEntityStatus HandleHistory(int assetId, int employeeId)
        {
            var history = new History();
            
            try
            {
                DateTime today = DateTime.Now.Date;
                history.CheckinDate = today;
                history.EmployeeID = employeeId;
                history.AssetID = assetId;
                AddEntity(history);

                return Enumerations.AddEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.AddEntityStatus.Failed;
            }           
        }

        public Enumerations.UpdateEntityStatus SaveCheckoutDate(int assetId, string remark)
        {
            var history = _historyRepository.GetHistoryByAssetId(assetId);

            try
            {
                DateTime today = DateTime.Now.Date;
                history.CheckoutDate = today;
                history.Remark = remark;
                UpdateEntity(history);

                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
        }
    }
}