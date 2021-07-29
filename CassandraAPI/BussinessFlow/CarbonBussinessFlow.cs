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

        public int TotalCarbon()
        {
            List<CarbonHistoryEntity> carbonHistory = this.baseRepository.Gets<CarbonHistoryEntity>();
            return carbonHistory.Select(a => a.carbonAmount).Sum();
        }

        public double TotalEarn(int userId)
        {
            List< OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a=>a.userId == userId);
            int totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes/60000)*0.0625;
            return TotalEarn;
        }

        public int CarbonTodayById(int userId)
        {
            return this.baseRepository.GetItem<CarbonPerDayEntity>(a=>a.userId == userId).carbonPerDay;
        }

        public int CarbonByDay(DateTime dateTime)
        {
            return this.baseRepository.Gets<CarbonPerDayEntity>(a => a.time.Date == dateTime.Date).Select(a => a.carbonPerDay).Sum();
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
        public double AvgCarbon()
        {
            return (double)TotalCarbon() / (double)this.baseRepository.Gets<CarbonHistoryEntity>().Select(a => a.distanceTotal).Sum();
        }
        public List<CarbonPerHourResponse> CarbonPerHour()
        {
            List<CarbonPerHourEntity> toDayCarbon = this.baseRepository.Gets<CarbonPerHourEntity>(a=>a.time.Date == DateTime.Now.Date);
            DateTime now = DateTime.Now;
            DateTime startTime = DateTime.Today.AddDays(-1).AddHours(23);
            List<CarbonPerHourResponse> carbonHours = new List<CarbonPerHourResponse>();
            int i = 0;
            while (startTime.Date != now.Date && startTime.Hour != now.Hour)
            {
                Console.WriteLine(now);
                List<CarbonPerHourEntity> temp = toDayCarbon.Where(a=>a.time.Hour == now.Hour).ToList();
                now = now.AddHours(-1);

                CarbonPerHourResponse carbonHour = new CarbonPerHourResponse()
                {
                    carbon = temp.Select(a=>a.carbon).Sum(),
                    dateTime = temp.Select(a=>a.time).FirstOrDefault()
                };
                carbonHours.Add(carbonHour);
            }
            return carbonHours.OrderBy(a => a.dateTime).ToList();
        }

        public List<CarbonPerHourEntity> createCarbonperhours(List<CarbonPerHourEntity> carbonPerHours)
        {
            return this.baseRepository.CreateList(carbonPerHours);
        }

        public List<CarbonPerDayEntity> createCarbonperDays(List<CarbonPerDayEntity> carbonPerDays)
        {
            return this.baseRepository.CreateList(carbonPerDays);
        }

        public CarbonHistoryEntity createCarbonHistory(CarbonHistoryEntity carbonHistory)
        {
            return this.baseRepository.Create(carbonHistory);
        }
        public List<CarbonPerHourResponse> CarbonPerHourFromDay(DateTime dateTime)
        {
            DateTime startTime = dateTime.Date;
            List<CarbonPerHourEntity> toDayCarbon = this.baseRepository.Gets<CarbonPerHourEntity>(a => a.time.Date == startTime);
            List<CarbonPerHourResponse> carbonHourDays = new List<CarbonPerHourResponse>();

            for (int i = 0; i <= 23; i++)
            {
                List<CarbonPerHourEntity> temp = toDayCarbon.Where(a => a.time == startTime).ToList();
                startTime = startTime.AddHours(1);
                CarbonPerHourResponse carbonPerHour = new CarbonPerHourResponse()
                {
                    carbon = temp.Select(a => a.carbon).Sum(),
                    dateTime = temp.Select(a => a.time).FirstOrDefault()
                };
                carbonHourDays.Add(carbonPerHour);
            }
            return carbonHourDays;
        }
    }
}
