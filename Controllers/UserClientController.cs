using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Royal_Pizza.API.Controllers
{
    [ApiController]
    public class CrunchyController : ControllerBase
    {
        [Route("users")]
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Users";
            string tmp = "[";
            var read = command.ExecuteReader();
            var first = true;

            while (read.Read())
            {
                for (int i = 0; i <= read.VisibleFieldCount - 1; i++)
                {
                    var r = read.GetValue(i);
                    var val = "";
                    if (i == 0 || i == 3)
                        val = r.ToString();
                    else
                        val = "\"" + r.ToString() + "\"";

                    if (first && i == 0)
                    {
                        tmp += @"{""id"": " + val + ",";
                        first = false;
                    }
                    else if (i == 0 && !first)
                        tmp += @",{""id"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""username"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""password"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""ClientID"": " + val + ",";
                }
                tmp += "}";
            }
            tmp += "]";
            return JsonConvert.DeserializeObject<List<User>>(tmp);
        }
        [Route("users/{user}")]
        [HttpGet]
        public ActionResult<User> Get(string user)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE username = '" + user + "'";
            string tmp = "";
            var read = command.ExecuteReader();

            while (read.Read())
            {
                for (int i = 0; i <= read.VisibleFieldCount - 1; i++)
                {
                    var r = read.GetValue(i);
                    var val = "";
                    if (i == 1 || i == 2)
                        val = "\"" + r.ToString() + "\"";
                    else
                        val = r.ToString();

                    if (i == 0)
                        tmp += @"{""id"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""username"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""password"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""ClientID"": " + val;
                }
                tmp += "}";
            }
            return JsonConvert.DeserializeObject<User>(tmp);
        }

        [Route("client/{id}")]
        [HttpGet]
        public ActionResult<Client> Get(int id)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Client WHERE id = " + id.ToString() + "";
            string tmp = "";
            var read = command.ExecuteReader();

            while (read.Read())
            {
                for (int i = 0; i <= read.VisibleFieldCount - 1; i++)
                {
                    var r = read.GetValue(i);
                    var val = "";
                    if (i == 1 || i == 2)
                        val = "\"" + r.ToString() + "\"";
                    else
                        val = r.ToString();

                    if (i == 0)
                        tmp += @"{""id"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""username"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""password"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""ClientID"": " + val;
                }
                tmp += "}";
            }
            return JsonConvert.DeserializeObject<Client>(tmp);
        }

        [Route("user/{id}")]
        [HttpPost]
        public ActionResult<string> ActivateOrDeactivateSession(int id)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE id = " + id.ToString() + "";
            int tmp = -1;
            var read = command.ExecuteReader();

            while (read.Read())
            {
                tmp = Convert.ToInt32(read.GetValue(4));
            }

            if (tmp == 0)
                tmp = 1;
            else
                tmp = 0;

            var updtCommand = db.dbConnection.CreateCommand();
            updtCommand.CommandText = "UPDATE Users SET Session = " + tmp + " WHERE id = " + id.ToString() + "";

            updtCommand.ExecuteNonQuery();
            
            return "Ok";

        }

    }
}

public partial class Client
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }


    }

//[JsonProperty("secciones", NullValueHandling = NullValueHandling.Ignore)]
public partial class User
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("ClientID")]
        public int ClientID { get; set; }

    }

