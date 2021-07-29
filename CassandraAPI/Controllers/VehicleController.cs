using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CassandraAPI.Repository;
using CassandraAPI.Models;
using System.Linq;
using CassandraAPI.BussinessFlow;

namespace CassandraAPI.Controllers
{
    public class VehicleController : ControllerBase
    {
        private readonly VehicleBussinessFlow _VehicleBussinessFlow;
        public VehicleController(VehicleBussinessFlow _VehicleBussinessFlow)
        {
            this._VehicleBussinessFlow = _VehicleBussinessFlow;
        }
        [HttpGet("/brands")]
        public List<BrandEntity> getBrands()
        {
            return _VehicleBussinessFlow.getBrands();
        }
        [HttpGet("/types")]
        public List<TypeEntity> getTypes()
        {
            return _VehicleBussinessFlow.getTypes();
        }

        [HttpGet("/models")]
        public List<ModelEntity> getmodels()
        {
            return _VehicleBussinessFlow.getModels();
        }

        [HttpGet("/user/vehicle")]
        public List<BrandEntity> getUserVehicles()
        {
            return _VehicleBussinessFlow.getBrands();
        }
    }
}
