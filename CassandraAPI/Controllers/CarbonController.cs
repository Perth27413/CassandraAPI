using CassandraAPI.BussinessFlow;
using CassandraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CassandraAPI.Controllers
{
    public class CarbonController : ControllerBase
    {
        private readonly CarbonBussinessFlow _carbonBussinessFlow;
        public CarbonController(CarbonBussinessFlow _carbonBussinessFlow)
        {
            this._carbonBussinessFlow = _carbonBussinessFlow;
        }
        [HttpGet("/carbon/history")]
        public List<CarbonHistoryEntity> CarbonHistory([FromQuery] int userId)
        {
            return _carbonBussinessFlow.CarbonHistoryById(userId);
        }

        [HttpGet("/carbon/avg")]
        public double CarbonAvg()
        {
            return _carbonBussinessFlow.AvgCarbon();
        }

        [HttpPost("/carbon/history")]
        public CarbonHistoryEntity createCarbonPerHour([FromBody] CarbonHistoryEntity carbonHistory)
        {
            return _carbonBussinessFlow.createCarbonHistory(carbonHistory);
        }

        [HttpGet("/carbon/range")]
        public object carbonRange([FromQuery] DateTime startTime, DateTime endTime)
        {
            return _carbonBussinessFlow.CarbonRange(startTime, endTime);
        }

        [HttpPost("/carbon/log")]
        public object createHistory([FromQuery] int userId, double distance)
        {
            return _carbonBussinessFlow.createHistory(userId, distance);
        }

        [HttpGet("/user/earn")]
        public double TotalEarn()
        {
            return _carbonBussinessFlow.TotalEarn();
        }

        [HttpPost("/user/time")]
        public OnlineTimeEntity CreateTime([FromQuery] int userId, double time)
        {
            return _carbonBussinessFlow.CreateOnlineTime(userId, time);
        }

        [HttpGet("/mobile/home")]
        public MobileHomeResponse GetInfoMobile([FromQuery] int userId)
        {
           return _carbonBussinessFlow.GetInfoMobile(userId);
        }

        [HttpGet("/mobile/history")]
        public List<CarbonHistoryEntity> GetHistory([FromQuery] int userId)
        {
            return _carbonBussinessFlow.GetHistory(userId);
        }

        [HttpGet("/carbon/perhourfromday")]
        public object getCarbonPerHourFromDay([FromQuery] DateTime dateTime)
        {
            return _carbonBussinessFlow.CarbonInDay(dateTime);
        }

    }
}
