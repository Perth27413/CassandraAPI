using CassandraAPI.Models;
using CassandraAPI.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CassandraAPI.BussinessFlow
{
    public class VehicleBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public VehicleBussinessFlow(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public List<BrandEntity> getBrands()
        {
            return this.baseRepository.Gets<BrandEntity>(); ;
        }
        public List<TypeEntity> getTypes()
        {
            return this.baseRepository.Gets<TypeEntity>(); ;
        }
        public List<ModelEntity> getModels()
        {
            return this.baseRepository.Gets<ModelEntity>(); ;
        }

        public string getUserVehicle(int id)
        {
            UserEntity userInfo = this.baseRepository.GetItem<UserEntity>(a => a.userId == id);
            VehicleEntity userVehicleInfo = this.baseRepository.GetInclude<VehicleEntity>(null, filter: a => a.vehicleId == userInfo.vehicle, includeProperties: "brandEntity, typeEntity, modelEntity").FirstOrDefault();
            string vehicle = userVehicleInfo.brandEntity.brand + " " + userVehicleInfo.modelEntity.model + " " + userVehicleInfo.typeEntity.type;
            return vehicle;
        }
    }
}
