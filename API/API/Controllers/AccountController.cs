using API.DTO_s;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private static string dbuser = "postgres";
        private static string dbpass = "Admin@123";
        private static string dbIp = "127.0.0.1";
        private static string dbname = "management";

        private string _dbConnectionString= $"User ID={dbuser};Password={dbpass};Server={dbIp};Port=5432;Database={dbname};Integrated Security=true;Pooling=true;Timeout=30;CommandTimeout=30;";
        

        [HttpGet]
        [Route("api/getName")]


        public List<dynamic> Name(User _user)
        {
            using(NpgsqlConnection _npgsql=new NpgsqlConnection(_dbConnectionString))
            {
                if (_npgsql.State != System.Data.ConnectionState.Open)
                    _npgsql.Open();
                string sqlQuery = @"select * from employee";

               List<dynamic> list = _npgsql.Query(sql: sqlQuery, commandTimeout: 60).ToList();
                return list;
            }        
           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/login")]
        public LoginRes Login([FromBody] person _person)
        {
            string Sqlquery = @"select * from person where username="+ "'" + _person.UserName + "'"+ " or email=" + "'" + _person.UserName + "'" + " and password= " + "'" + _person.Password + "'";
            List<dynamic> list = new List<dynamic>();
            using (NpgsqlConnection _npgsql = new NpgsqlConnection(_dbConnectionString))
            {
                if (_npgsql.State != System.Data.ConnectionState.Open)
                    _npgsql.Open();
                list = _npgsql.Query(sql: Sqlquery, commandTimeout: 60).ToList();               
            }
            _person.Id= list[0].id;
            _person.UserName= list[0].username;
            _person.EmailAddress= list[0].username;
            _person.Password = list[0].password;
            var LogResDto = new LoginRes();
            if (list.Count == 1)
            {
                LogResDto.UserName = _person.UserName;

                LogResDto.Token = CreateJWT(_person);
            }
            
            return LogResDto;

        }

        [AllowAnonymous]
        public string CreateJWT(person _person)
        {
            var TopSecretKey = configuration.GetSection("AppSetting:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TopSecretKey));
            var claims = new Claim[]
            { 
                new Claim(ClaimTypes.Name, _person.UserName),
                new Claim(ClaimTypes.NameIdentifier,_person.Id.ToString())
            };
            var SigningCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = SigningCredential
            };
            var TokenHanler = new JwtSecurityTokenHandler();
            var Token = TokenHanler.CreateToken(TokenDescriptor);
            return TokenHanler.WriteToken(Token);
        }

    }
}
