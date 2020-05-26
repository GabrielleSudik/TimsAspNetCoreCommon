using DataLibrary.DB;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class FoodData : IFoodData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        //Inject the stuff you'll need:
        public FoodData(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        //a method to call LoadData, sending the stored proc we need.
        //we get back a Task (not just a List) because this method calls an async method.
        public Task<List<FoodModel>> GetFood()
        {
            return _dataAccess.LoadData<FoodModel, dynamic>("dbo.spFood_All",
                                                            new { }, //"new {}" is in the parameters position. 
                                                            _connectionString.SqlConnectionName);
            //"dynamic" allows the passing of an anonymous object. Which is what "new {}" is.
        }
    }
}

//Last step: Extract an interface from this class.
