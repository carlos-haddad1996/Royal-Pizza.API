using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Royal_Pizza.API.Controllers
{
    [ApiController]
    public class DrinksController : ControllerBase
    {

        [Route("drinks")]
        [HttpGet]
        public ActionResult<List<Drinks>> Get()
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Drinks";
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
            return JsonConvert.DeserializeObject<List<Drinks>>(tmp);
        }

        [Route("drink/{id}")]
        [HttpGet]
        public ActionResult<Drinks> Get(int id)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Drinks WHERE id = " + id.ToString() + "";
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

            return JsonConvert.DeserializeObject<Drinks>(tmp);
        }

    }
}

public partial class Drinks
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
