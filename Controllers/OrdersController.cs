using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Royal_Pizza.API.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {

        [Route("orders")]
        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            Database db = new Database();
            db.dbConnection.Open();
            var command = db.dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Orders";
            string tmp = "[";
            var read = command.ExecuteReader();
            var first = true;

            while (read.Read())
            {
                for (int i = 0; i <= read.VisibleFieldCount - 1; i++)
                {
                    var r = read.GetValue(i);
                    var val = "";
                    if (i == 0 || i == 1)
                        val = r.ToString();
                    else
                        val = r.ToString();

                    if (first && i == 0)
                    {
                        tmp += @"{""id"": " + val + ",";
                        first = false;
                    }
                    else if (i == 0 && !first)
                        tmp += @",{""id"": " + val + ",";
                    else if (i == 1)
                        tmp += @"""ClientID"": " + val + ",";
                    else if (i == 2)
                    {
                        val = "[" + val;
                        val += "]";
                        tmp += @"""pizzas"": " + val + ",";
                    }
                    else if (i == 3)
                    {
                        val = "[" + val;
                        val += "]";
                        tmp += @"""desserts"": " + val + ",";
                    }
                    else if (i == 4)
                    {
                        val = "[" + val;
                        val += "]";
                        tmp += @"""drinks"": " + val;
                    }
                        
                }
                tmp += "}";
            }
            tmp += "]";
            return JsonConvert.DeserializeObject<List<Order>>(tmp);
        }

    }
}

public partial class Order
{
    [JsonProperty("id")]
    public int id { get; set; }

    [JsonProperty("clientID")]
    public int clientID { get; set; }

    [JsonProperty("pizzas")]
    public int[] pizzas { get; set; }

    [JsonProperty("desserts")]
    public int [] desserts { get; set; }

    [JsonProperty("drinks")]
    public int[] drinks { get; set; }

}
