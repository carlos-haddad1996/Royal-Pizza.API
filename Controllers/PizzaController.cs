﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Royal_Pizza.API.Controllers
{
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [Route("pizzas")]
        [HttpGet]
        public ActionResult<List<Pizza>> Get()
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Pizzas";
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
                        tmp += @"""name"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""description"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""price"": " + val + ",";
                    else if (i == 4)
                        tmp += @"""image"": " + val;
                }
                tmp += "}";
            }
            tmp += "]";
            return JsonConvert.DeserializeObject<List<Pizza>>(tmp);
        }

        [Route("pizza/{id}")]
        [HttpGet]
        public ActionResult<Pizza> Get(int id)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Pizzas WHERE id = " + id.ToString() + "";
            string tmp = "";
            var read = command.ExecuteReader();

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

                    if (i == 0)
                        tmp += @"{""id"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""name"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""description"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""price"": " + val + ",";
                    else if (i == 4)
                        tmp += @"""image"": " + val;
                }
                tmp += "}";
            }

            return JsonConvert.DeserializeObject<Pizza>(tmp);

        }
    }
}

public partial class Pizza
{
    [JsonProperty("id")]
    public int id { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("description")]
    public string description { get; set; }

    [JsonProperty("price")]
    public double price { get; set; }

    [JsonProperty("image")]
    public string image { get; set; }
}
