using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CassandraAPI.Models;
using CassandraAPI.Repository;
using CassandraAPI.BussinessLogic;
using CassandraAPI.BussinessFlow;
using System.Linq;

namespace CassandraAPI.BussinessFlow
{
    public class LoginBussinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public LoginBussinessFlow(IBaseRepository baseRepository)
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
                vehicle = regis.vehicle,
                position = 1
            };
            return this.baseRepository.Create<UserEntity>(newUser); ;
        }
    }
}
