using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Royal_Pizza.API.Controllers
{
    [Route("users/{user}")]
    [ApiController]
    public class CrunchyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get(string user)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Username = '"+user+"'";
            string tmp = "";
            var read = command.ExecuteReader();

            while(read.Read())
            {
                for(int i = 0; i <= read.VisibleFieldCount-1; i++)
                {
                    var r = read.GetValue(i);
                    if (i == 0)
                        tmp += @"{""User_ID"": "+r.ToString()+",";
                    else if(i == 1)
                        tmp += @"""Username"": "+r.ToString()+",";
                    else if(i == 2)
                        tmp += @"""Password"": "+r.ToString();
                    else if(i == 3)
                        tmp += @"""Client_ID"": " + r.ToString();
                }
                tmp += "}";
            }
            return tmp;
            //JsonConvert.DeserializeObject<List<Pizza>>(Convert.ToString(r));
        }
    }

    public partial class Pizza
    {

    }

}
