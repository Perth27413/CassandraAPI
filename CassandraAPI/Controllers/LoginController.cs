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
        public UserEntity UserRegister(RegisterRequest loginRequest)
        {
            return _bussinessFlow.UserRegister(loginRequest);
        }
    }
}
