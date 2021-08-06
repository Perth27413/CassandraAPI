using CassandraAPI.Models;
using CassandraAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public double TotalCarbon()
        {
            List<CarbonHistoryEntity> carbonHistory = this.baseRepository.Gets<CarbonHistoryEntity>();
            return carbonHistory.Select(a => a.carbonAmount).Sum();
        }

        public double TotalEarn()
        {
            List<OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>();
            double totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes / 3600) * 0.125;
            return TotalEarn;
        }
       
        public double AvgCarbon()
        {
            return (double)TotalCarbon() / (double)this.baseRepository.Gets<CarbonHistoryEntity>().Select(a => a.distanceTotal).Sum();
        }

        public CarbonHistoryEntity createCarbonHistory(CarbonHistoryEntity carbonHistory)
        {
            return this.baseRepository.Create(carbonHistory);
        }
/*        public List<CarbonPerHourResponse> CarbonPerHourFromDay(DateTime dateTime)
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
        }*/
        public object CarbonRange(DateTime startTime, DateTime EndTime)
        {
            List<CarbonHistoryEntity> rangeDayCarbon = this.baseRepository.GetInclude<CarbonHistoryEntity>(null, filter:a => a.createdAt.Date >= startTime.Date && a.createdAt <= EndTime);
            return rangeDayCarbon.GroupBy(a => a.createdAt.Date).Select(a => new { carbon = a.Sum(b => b.carbonAmount), dateTime = a.Key.Date }).OrderBy(a => a.dateTime).ToList();
        }

        public object CarbonInDay(DateTime dateTime)
        {
            List<CarbonHistoryEntity> rangeCarbonToday = this.baseRepository.GetInclude<CarbonHistoryEntity>(null, filter:a=>a.createdAt.Date >= dateTime.Date && a.createdAt <= DateTime.Now);
            return rangeCarbonToday.GroupBy(a => a.createdAt.Hour).Select(a => new { carbon = a.Sum(b => b.carbonAmount), dateTime = a.Key<10?"0"+a.Key+":00:00": a.Key + ":00:00" }).OrderBy(a => a.dateTime).ToList();
        }

        public double calculateCarbon(int userId, double distance)
        {
            UserEntity userInfo = this.baseRepository.GetInclude<UserEntity>(null, filter: a => a.userId == userId, includeProperties: "vehicleEntity").FirstOrDefault();
            double co2 = userInfo.vehicleEntity.co2;
            return co2*distance;
        }

        public CarbonHistoryEntity createHistory(int userId, double distance,double tripTime)
        {
            double carbon = calculateCarbon(userId, distance);
            CarbonHistoryEntity history = new CarbonHistoryEntity()
            {
                carbonAmount = carbon,
                distanceTotal = distance,
                userId = userId,
                userEntity = this.baseRepository.GetItem<UserEntity>(a => a.userId == userId),
                tripTime = tripTime,
                createdAt = DateTime.Now
            };
            UserCarbonEntity userCarbon = this.baseRepository.GetItem<UserCarbonEntity>(a => a.userId == userId);
            CarbonHistoryEntity response = this.baseRepository.Create<CarbonHistoryEntity>(history);
            if (userCarbon == null)
            {
                UserCarbonEntity newUser = new UserCarbonEntity() { 
                    userId = userId,
                    carbon = 0
                };
                userCarbon = this.baseRepository.Create(newUser);
            }
                userCarbon.carbon += response.carbonAmount;
                this.baseRepository.Update(userCarbon);
            return response;
        }

        public OnlineTimeEntity CreateOnlineTime(int userId, double time)
        {
            OnlineTimeEntity onlineTime = this.baseRepository.GetItem<OnlineTimeEntity>(a => a.userId == userId);
            onlineTime.timeOnline += time;
            onlineTime.createdAt = DateTime.Now;
            return this.baseRepository.Update(onlineTime);
        }

        public MobileHomeResponse GetInfoMobile(int userId)
        {
            List<CarbonHistoryEntity> userHistory = this.baseRepository.GetInclude<CarbonHistoryEntity>(null, a=>a.userId == userId);
            double carbonTotal = userHistory.Select(a => a.carbonAmount).Sum();
            double carbonToday = userHistory.Where(a => a.createdAt.Date == DateTime.Today).Select(a => a.carbonAmount).Sum();
            double distance = userHistory.Select(a => a.distanceTotal).Sum();
            double carbonAvg = 0;
            if (distance!=0)
            {
                carbonAvg = carbonTotal / distance;
            }
            List<OnlineTimeEntity> onlineTimes = this.baseRepository.GetInclude<OnlineTimeEntity>(null, a => a.userId == userId);
            double earnTotal = (((onlineTimes.Select(a => a.timeOnline).Sum()/3600)*0.125)/30);
            double earnToday = (((onlineTimes.Where(a => a.createdAt.Date == DateTime.Today).Select(a => a.timeOnline).Sum()/3600)*0.125)/30);
            UserEntity userEntity = this.baseRepository.GetInclude<UserEntity>(null, filter: a => a.userId == userId, includeProperties: "positionEntity").FirstOrDefault();
            userEntity.vehicleEntity = this.baseRepository.GetInclude<VehicleEntity>(null, filter:a=>a.vehicleId == userEntity.vehicle, includeProperties: "brandEntity,typeEntity,modelEntity").FirstOrDefault();
            MobileHomeResponse response = new MobileHomeResponse()
            {
                TodayCarbon = carbonToday,
                TodayEarn = earnToday,
                TotalCarbon = carbonTotal,
                TotalEarn = earnTotal,
                AvgCarbon = carbonAvg,
                TotalTime = onlineTimes.Select(a => a.timeOnline).Sum(),
                UserInfo = userEntity
            };
            return response;
        }

        public List<CarbonHistoryEntity> GetHistory(int userId)
        {
            return this.baseRepository.GetInclude<CarbonHistoryEntity>(null, filter: a => a.userId == userId && a.createdAt.Date == DateTime.Today).OrderByDescending(a => a.createdAt).ToList();
        }

    }
}
