using CassandraAPI.Models;
using CassandraAPI.Repository;
using System.Linq;

namespace CassandraAPI.BussinessFlow
{
    public class HealthCheckBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public HealthCheckBussinessFlow(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public string HealthCheck()
        {
            return this.baseRepository.Gets<HealthCheckEntity>().FirstOrDefault().message;
        }
    }
}
