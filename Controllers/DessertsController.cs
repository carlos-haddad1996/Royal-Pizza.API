using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Royal_Pizza.API.Controllers
{
    [ApiController]
    public class DessertsController : ControllerBase
    {
        [Route("desserts")]
        [HttpGet]
        public ActionResult<List<Dessert>> Get()
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Dessert";
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
                        tmp += @"{""Dessert_ID"": " + val + ",";
                        first = false;
                    }
                    else if (i == 0 && !first)
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
            tmp += "]";
            return JsonConvert.DeserializeObject<List<Dessert>>(tmp);
        }

        [Route("dessert/{id}")]
        [HttpGet]
        public ActionResult<Dessert> Get(int id)
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Dessert WHERE Dessert_ID = " + id.ToString() + "";
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
                        tmp += @"{""Dessert_ID"": " + val + ",";
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

            return JsonConvert.DeserializeObject<Dessert>(tmp);

        }

    }
}

public partial class Dessert
{
    [JsonProperty("Dessert_ID")]
    public int Dessert_ID { get; set; }

    [JsonProperty("DessertName")]
    public string DessertName { get; set; }

    [JsonProperty("DessertDescription")]
    public string DessertDescription { get; set; }

    [JsonProperty("DessertPrice")]
    public double DessertPrice { get; set; }

    [JsonProperty("ImageURL")]
    public string ImageURL { get; set; }

}
