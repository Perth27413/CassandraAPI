using CassandraAPI.BussinessFlow;
using Microsoft.AspNetCore.Mvc;
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
