﻿using System;
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
                        tmp += @"{""User_ID"": " + val + ",";
                        first = false;
                    }
                    else if (i == 0 && !first)
                        tmp += @",{""User_ID"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""Username"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""Password"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""Client_ID"": " + val + ",";
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
            command.CommandText = "SELECT * FROM Users WHERE Username = '"+user+"'";
            string tmp = "";
            var read = command.ExecuteReader();

            while(read.Read())
            {
                for(int i = 0; i <= read.VisibleFieldCount-1; i++)
                {
                    var r = read.GetValue(i);
                    var val = "";
                    if (i == 1 || i == 2)
                        val = "\"" + r.ToString() + "\"";
                    else
                        val = r.ToString();

                    if (i == 0)
                        tmp += @"{""User_ID"": "+val+",";
                    else if(i == 1)
                        tmp += @"""Username"": " + val + ",";
                    else if(i == 2)
                        tmp += @"""Password"": "+ val + ",";
                    else if(i == 3)
                        tmp += @"""Client_ID"": " + val;
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
            command.CommandText = "SELECT * FROM Client WHERE Client_ID = " + id.ToString() + "";
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
                        tmp += @",{""Dessert_ID"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""DessertName"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""DessertDescription"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""DessertPrice"": " + val + ",";
                    else if (i == 4)
                        tmp += @"""ImageURL"": " + val;
                }
                tmp += "}";
            }
            return JsonConvert.DeserializeObject<Client>(tmp);
        }

    }

    public partial class Client
    {
        [JsonProperty("Client_ID")]
        public int Client_ID { get; set; }

        [JsonProperty("Client_Name")]
        public string Client_Name { get; set; }

        [JsonProperty("Client_LastName")]
        public string Client_LastName { get; set; }

        [JsonProperty("Client_Age")]
        public int Client_Age { get; set; }


    }

    //[JsonProperty("secciones", NullValueHandling = NullValueHandling.Ignore)]
    public partial class User
    {
        [JsonProperty("User_ID")]
        public int User_ID { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Client_ID")]
        public int Client_ID { get; set; }

    }

}
