using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CassandraAPI.Repository;
using CassandraAPI.Models;
using CassandraAPI.BussinessFlow;

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

        [HttpGet("/carbon/byDay")]
        public int CarbonToday([FromQuery] DateTime dateTime)
        {
            return _carbonBussinessFlow.CarbonByDay(dateTime);
        }

        [HttpGet("/carbon/avg")]
        public double CarbonAvg()
        {
            return _carbonBussinessFlow.AvgCarbon();
        }

        [HttpGet("/carbon/perhour")]
        public List<CarbonPerHourResponse> CarbonPerHour()
        {
            return _carbonBussinessFlow.CarbonPerHour();
        }

        [HttpGet("/carbon/perhourfromday")]
        public List<CarbonPerHourResponse> CarbonPerHourFromDay([FromQuery]DateTime dateTime)
        {
            return _carbonBussinessFlow.CarbonPerHourFromDay(dateTime);
        }

        [HttpPost("/carbon/perhour")]
        public List<CarbonPerHourEntity> createCarbonPerHour([FromBody] List<CarbonPerHourEntity> carbonPerHour)
        {
            return _carbonBussinessFlow.createCarbonperhours(carbonPerHour);
        }

        [HttpPost("/carbon/perday")]
        public List<CarbonPerDayEntity> createCarbonPerday([FromBody] List<CarbonPerDayEntity> carbonPerDay)
        {
            return _carbonBussinessFlow.createCarbonperDays(carbonPerDay);
        }

        [HttpPost("/carbon/history")]
        public CarbonHistoryEntity createCarbonPerHour([FromBody] CarbonHistoryEntity carbonHistory)
        {
            return _carbonBussinessFlow.createCarbonHistory(carbonHistory);
        }
    }
}
