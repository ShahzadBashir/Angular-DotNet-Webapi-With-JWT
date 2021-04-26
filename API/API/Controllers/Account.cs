using API.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class Account : ControllerBase
    {
        [HttpGet]
        [Route("api/getName")]


        public User Name(User _user)
        {
            _user.Name = "Shahzad";
            return _user;
        }

    }
}
