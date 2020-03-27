using System;
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
                        tmp += @"{""PizzaID"": " + val + ",";
                        first = false;
                    }
                    else if (i == 0 && !first)
                        tmp += @",{""PizzaID"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""PizzaName"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""PizzaDescription"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""PizzaPrice"": " + val + ",";
                    else if (i == 4)
                        tmp += @"""ImageURL"": " + val;
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
            command.CommandText = "SELECT * FROM Pizzas WHERE PizzaID = " + id.ToString() + "";
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
                        tmp += @"{""PizzaID"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""PizzaName"": " + val + ",";
                    else if (i == 2)
                        tmp += @"""PizzaDescription"": " + val + ",";
                    else if (i == 3)
                        tmp += @"""PizzaPrice"": " + val + ",";
                    else if (i == 4)
                        tmp += @"""ImageURL"": " + val;
                }
                tmp += "}";
            }

            return JsonConvert.DeserializeObject<Pizza>(tmp);

        }
    }
}

public partial class Pizza
{
    [JsonProperty("PizzaID")]
    public int PizzaID { get; set; }

    [JsonProperty("PizzaName")]
    public string PizzaName { get; set; }

    [JsonProperty("PizzaDescription")]
    public string PizzaDescription { get; set; }

    [JsonProperty("PizzaPrice")]
    public double PizzaPrice { get; set; }

    [JsonProperty("ImageURL")]
    public string ImageURL { get; set; }
}
