using System;
using System.Data.SQLite;
namespace Royal_Pizza.API.Functions
{
    [SQLiteFunction(Name = "JSON", Arguments = 1, FuncType = FunctionType.Scalar)]
    class JSON : SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            string rString = "";

            return rString;
        }
    }
}
