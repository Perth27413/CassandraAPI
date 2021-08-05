using CassandraAPI.BussinessFlow;
using CassandraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CassandraAPI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly LoginBussinessFlow _bussinessFlow;
        public LoginController(LoginBussinessFlow _bussinessFlow)
        {
            this._bussinessFlow = _bussinessFlow;
        }
        [HttpGet("/login")]
        public bool UserLogin(LoginRequest loginRequest)
        {
            return _bussinessFlow.LoginCheck(loginRequest);
        }

        [HttpPost("/Register")]
        public UserEntity UserRegister([FromBody]RegisterRequest loginRequest)
        {
            return _bussinessFlow.UserRegister(loginRequest);
        }

        [HttpGet("/user/all")]
        public List<UserCarbonEntity> getUser()
        {
            return _bussinessFlow.getAllUser();
        }

        [HttpGet("/user/vehicle")]
        public object getVehicle()
        {
            return _bussinessFlow.getVehicle();
        }
    }
}
