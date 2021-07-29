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
    public class HealthCheckController : ControllerBase
    {
        private readonly HealthCheckBussinessFlow _healthCheckBussinessFlow;
        public HealthCheckController(HealthCheckBussinessFlow _healthCheckBussinessFlow)
        {
            this._healthCheckBussinessFlow = _healthCheckBussinessFlow;
        }
        [HttpGet("/HealthCheck")]
        public string HealthCheck()
        {
            return _healthCheckBussinessFlow.HealthCheck();
        }
    }
}
