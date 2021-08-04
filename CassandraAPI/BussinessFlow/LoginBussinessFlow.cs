using CassandraAPI.Data;
using CassandraAPI.Models;
using CassandraAPI.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CassandraAPI.BussinessFlow
{
    public class LoginBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        private readonly MainContext context;
        public LoginBussinessFlow(IBaseRepository baseRepository, MainContext context)
        {
            this.baseRepository = baseRepository;
            this.context = context;
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
            return this.baseRepository.Create<UserEntity>(newUser);
        }

        public List<UserCarbonEntity> getAllUser()
        {
            return this.baseRepository.GetInclude<UserCarbonEntity>(null, includeProperties: "userEntity").OrderBy(a=>a.carbon).ToList();
        }
    }
}
