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

        public int TotalCarbon(int userId)
        {
            return this.baseRepository.GetItem<CarbonRankingEntity>(a=>a.userId == userId).carbonAmount;
        }

        public double TotalEarn(int userId)
        {
            List< OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a=>a.userId == userId);
            int totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes/60000)*0.0625;
            return TotalEarn;
        }

        public int TotalToday(int userId)
        {
            return this.baseRepository.GetItem<CarbonPerDayEntity>(a=>a.userId == userId).carbonPerDay;
        }

        public double TodayEarn(int userId)
        {
            DateTime today = DateTime.Now;
            //List<OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<CarbonPerHourEntity>(a => a.userId == userId && a);
            return this.baseRepository.GetItem<CarbonPerDayEntity>(a => a.userId == userId).carbonPerDay;
        }
    }
}
