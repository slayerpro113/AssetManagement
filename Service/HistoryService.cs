using System.Collections.Generic;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;

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
    }
}