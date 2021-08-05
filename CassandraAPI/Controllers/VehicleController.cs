using CassandraAPI.BussinessFlow;
using CassandraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    }
}
