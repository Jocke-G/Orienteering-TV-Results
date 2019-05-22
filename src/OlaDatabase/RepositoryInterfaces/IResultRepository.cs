using OlaDatabase.Entities;
using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IResultRepository
    {
        IList<ResultEntity> GetBy(int eventRaceId, int eventClassId);
    }
}