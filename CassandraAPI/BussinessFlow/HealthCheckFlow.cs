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
