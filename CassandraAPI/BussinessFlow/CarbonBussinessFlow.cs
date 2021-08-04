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

        public double TotalCarbonById(int userId)
        {
            List<CarbonHistoryEntity> carbonHistory = this.baseRepository.Gets<CarbonHistoryEntity>(a => a.userId == userId);
            return carbonHistory.Select(a=>a.carbonAmount).Sum();
        }

        public double TotalCarbon()
        {
            List<CarbonHistoryEntity> carbonHistory = this.baseRepository.Gets<CarbonHistoryEntity>();
            return carbonHistory.Select(a => a.carbonAmount).Sum();
        }
        ////---------------------
        public double TotalEarnById(int userId)
        {
            List< OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a=>a.userId == userId);
            double totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes/60)*0.125;
            return TotalEarn;
        }
        public double TotalEarn()
        {
            List<OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>();
            double totalTimes = userTimeInfo.Select(a => a.timeOnline).Sum();
            double TotalEarn = (totalTimes / 60) * 0.125;
            return TotalEarn;
        }

        public double CarbonTodayById(int userId)
        {
            return this.baseRepository.GetItem<CarbonPerDayEntity>(a=>a.userId == userId).carbonPerDay;
        }

        public double CarbonByDay(DateTime dateTime)
        {
            return this.baseRepository.Gets<CarbonPerDayEntity>(a => a.time.Date == dateTime.Date).Select(a => a.carbonPerDay).Sum();
        }

        public double TodayEarnByIdPerDay(int userId)
        {
            DateTime today = DateTime.Now.Date;
            List<OnlineTimeEntity> userTimeInfo = this.baseRepository.Gets<OnlineTimeEntity>(a => a.userId == userId).ToList();
            double time = userTimeInfo.Where(a=>a.createdAt.Date == today).Select(a => a.timeOnline).Sum();
            return (time/60)*0.125;
        }
        ////-----------------------------------------
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
        public object CarbonRange(DateTime startTime, DateTime EndTime)
        {
            List<CarbonPerHourEntity> rangeDayCarbon = this.baseRepository.Gets<CarbonPerHourEntity>(a => a.time.Date >= startTime && a.time <= EndTime);
            return rangeDayCarbon.GroupBy(a => a.time.Date).Select(a => new { carbon = a.Sum(b => b.carbon), dateTime = a.Key.Date }).OrderBy(a => a.dateTime).ToList();
        }

        public CarbonPerDayEntity updateCarbonPerDay(DateTime date, int carbon, double distance)
        {
            CarbonPerDayEntity todayCabon = this.baseRepository.GetItem<CarbonPerDayEntity>(a=>a.time.Date == date.Date);
            if (todayCabon == null)
            {
                todayCabon.carbonPerDay = carbon;
                todayCabon.distanceTotal = distance;
                return this.baseRepository.Create(todayCabon);
            }
            return todayCabon;
        }

        public double calculateCarbon(int userId, double distance)
        {
            UserEntity userInfo = this.baseRepository.GetInclude<UserEntity>(null, filter: a => a.userId == userId, includeProperties: "vehicleEntity").FirstOrDefault();
            double co2 = userInfo.vehicleEntity.co2;
            return co2*distance;
        }

        public CarbonHistoryEntity createHistory(int userId, double distance)
        {
            double carbon = calculateCarbon(userId, distance);
            CarbonHistoryEntity history = new CarbonHistoryEntity()
            {
                carbonAmount = carbon,
                distanceTotal = distance,
                userId = userId,
                userEntity = this.baseRepository.GetItem<UserEntity>(a => a.userId == userId),
                tripTime = new Random().Next(5, 30),
                createdAt = DateTime.Now
            };
            UserCarbonEntity userCarbon = this.baseRepository.GetItem<UserCarbonEntity>(a => a.userId == userId);
            CarbonHistoryEntity response = this.baseRepository.Create<CarbonHistoryEntity>(history);
            if (userCarbon == null)
            {
                UserCarbonEntity newUser = new UserCarbonEntity() { 
                    carbon =carbon
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
            List<CarbonHistoryEntity> userHistory = this.baseRepository.Gets<CarbonHistoryEntity>(a=>a.userId == userId);
            double carbonTotal = userHistory.Select(a => a.carbonAmount).Sum();
            double carbonToday = userHistory.Where(a => a.createdAt.Date == DateTime.Today).Select(a => a.carbonAmount).Sum();
            double carbonAvg = carbonTotal / userHistory.Select(a => a.distanceTotal).Sum();
            List<OnlineTimeEntity> onlineTimes = this.baseRepository.Gets<OnlineTimeEntity>(a => a.userId == userId);
            double earnTotal = ((onlineTimes.Select(a => a.timeOnline).Sum()/60)*0.125);
            double earnToday = ((onlineTimes.Where(a => a.createdAt.Date == DateTime.Today).Select(a => a.timeOnline).Sum()/60)*0.125);
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
/*                vehicle = vehicle,*/
                UserInfo = userEntity
            };
            return response;
        }

        public List<CarbonHistoryEntity> GetHistory(int userId)
        {
            return this.baseRepository.GetInclude<CarbonHistoryEntity>(null, filter: a => a.userId == userId && a.createdAt.Date == DateTime.Today).OrderBy(a => a.createdAt).ToList();
        }

    }
}
