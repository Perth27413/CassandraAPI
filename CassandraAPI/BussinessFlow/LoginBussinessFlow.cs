using CassandraAPI.Data;
using CassandraAPI.Models;
using CassandraAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CassandraAPI.BussinessFlow
{
    public class LoginBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public LoginBussinessFlow(IBaseRepository baseRepository, MainContext context)
        {
            this.baseRepository = baseRepository;
        }
        public bool LoginCheck(LoginRequest loginRequest)
        {
            UserEntity user = this.baseRepository.Gets<UserEntity>(a => a.userName == loginRequest.userName && a.password == loginRequest.password).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public UserEntity UserRegister(RegisterRequest regis)
        {
            UserEntity newUser = new UserEntity()
            {
                userName = regis.userName,
                password = regis.password,
                firstName = regis.firstName,
                lastName = regis.lastName,
                vehicleYear = regis.year,
                vehicle = 1,
                position = 1,
                profilePic = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2F365webresources.com%2Fwp-content%2Fuploads%2F2016%2F09%2FFREE-PROFILE-AVATARS.png&f=1&nofb=1"
            };
            UserEntity response = this.baseRepository.Create<UserEntity>(newUser);
            OnlineTimeEntity onlineTime = new OnlineTimeEntity()
            {
                userId = response.userId,
                createdAt = DateTime.Now,
                timeOnline = 0
            };
            this.baseRepository.Create(new OnlineTimeEntity()
            {
                userId = response.userId,
                createdAt = DateTime.Now,
                timeOnline = 0
            }
            );

            return this.baseRepository.Create<UserEntity>(newUser);
        }

        public List<UserCarbonEntity> getAllUser()
        {
            return this.baseRepository.GetInclude<UserCarbonEntity>(null, includeProperties: "userEntity").OrderBy(a=>a.carbon).ToList();
        }

        public object getVehicle()
        {
            List < VehicleEntity > vehicleEntities = this.baseRepository.GetInclude<VehicleEntity>(null, includeProperties: "brandEntity,typeEntity,modelEntity");
            List<VehicleResponse> responses = new List<VehicleResponse>();
            object response = vehicleEntities.GroupBy(a => a).Select(a => new { 
                brand = a.GroupBy(b=>b.brandEntity).Select(b=> new { 
                    brandId = b.Key.brandId, brand = b.Key.brand, type = a.GroupBy(c=>c.typeEntity).Select(c=>new { 
                        typeId = c.Key.typeId, type=c.Key.type, model = a.GroupBy(d=>d.modelEntity).Select(d=>new { 
                            modelId = d.Key.modelId, model = d.Key.model})
                    })
                })
            });
            return response;
        }

    }
}
