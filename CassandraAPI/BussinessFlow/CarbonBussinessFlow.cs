using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CassandraAPI.Models;
using CassandraAPI.Repository;
using CassandraAPI.BussinessLogic;
using CassandraAPI.BussinessFlow;

namespace CassandraAPI.BussinessFlow
{
    public class CarbonBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public CarbonBussinessFlow(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public List<CarbonHistoryEntity> CarbonHistoryById(int userId)
        {
            Func<IQueryable<CarbonHistoryEntity>, IOrderedQueryable<CarbonHistoryEntity>> orderBy = m => m.OrderBy(a => a.createdAt);       
            return this.baseRepository.GetInclude<CarbonHistoryEntity>(null, orderBy: orderBy, filter: a => a.userId == userId);
        }

        public int TotalCarbonById(int userId)
        {
            List<CarbonHistoryEntity> carbonHistory = this.baseRepository.Gets<CarbonHistoryEntity>(a => a.userId == userId);
            return carbonHistory.Select(a=>a.carbonAmount).Sum();
        }

        public double TotalEarn(int userId)
        {
            List< OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a=>a.userId == userId);
            int totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes/60000)*0.0625;
            return TotalEarn;
        }

        public int TotalTodayById(int userId)
        {
            return this.baseRepository.GetItem<CarbonPerDayEntity>(a=>a.userId == userId).carbonPerDay;
        }

        public double TodayEarnById(int userId)
        {
            DateTime today = DateTime.Now.Date;
            List<OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a => a.userId == userId).ToList();
            int time = userTimeInfo.Where(a=>a.createdAt.Date == today).Select(a => a.timeOnline).Sum();
            return (time/60000)*0.0625;
        }
        public double AvgCarbonbyId(int userId)
        {
            return TotalCarbonById(userId)/this.baseRepository.Gets<CarbonHistoryEntity>(a=>a.userId == userId).Select(a=>a.distanceTotal).Sum();
        }
    }
}
