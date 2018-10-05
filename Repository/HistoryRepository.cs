using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class HistoryRepository
    {
        public static IList<History> GetHistoriesByAssetId(this IRepository<History> repository, int assetId)
        {
            var histories = repository.Entity.Where(_ => _.AssetID == assetId).ToList();
            return histories;
        }

    }
}
