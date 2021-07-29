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
        public List<CarbonHistoryEntity> CarbonHistory(int userId)
        {
            return _carbonBussinessFlow.CarbonHistoryById(userId);
        }
    }
}
