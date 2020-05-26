using Dapper;
using DataLibrary.DB;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderData(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateOrder(OrderModel order)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("OrderName", order.OrderName);
            p.Add("OrderDate", order.OrderDate);
            p.Add("FoodId", order.FoodId);
            p.Add("Quantity", order.Quantity);
            p.Add("Total", order.Total);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);
            //this last one is where a new order creates an Id (recall: it's created by the db)
            //and is passed back out from the db instead of passed to it. SCOPE_IDENTITY thing.

            await _dataAccess.SaveData("dbo.spOrders_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id"); //this looks for "Id" in the list of params.
        }

        public Task<int> UpdateOrderName(int orderId, string orderName)
        {
            return _dataAccess.SaveData("dbo.spOrders_Update",
                                        new { Id = orderId, OrderName = orderName },
                                        _connectionString.SqlConnectionName);

            //"new { Id = orderId } is an anonymous parameter, and we use it to pass the args the stored proc expects.
            //ie, we identify it as Id and pass the value too.
            //Ditto Order Name.

            //Hmm... maybe the anonymous object thing: Remember how in those methods, we don't explicity say what args will be passed.
            //We just have to make sure we pass the right things? Maybe anon objects are like "this part of the code expects something, ...
            //which might change depending on other params, and that other part of the code will have to make sure it's correct."
            //Ie, it's a flexible/dynamic parameter, just called "anonymous"?
        }

        public Task<int> DeleteOrder(int orderId)
        {
            return _dataAccess.SaveData("dbo.spOrders_Delete",
                                        new { Id = orderId },
                                        _connectionString.SqlConnectionName);
        }

        public async Task<OrderModel> GetOrderById(int orderId)
        {
            //We use await here because we actually need to wait on the results of this method call.
            //above, the underlying methods were async, but we didn't have to wait for them.
            //Here, we are getting info, not letting code run in the background.
            var recs = await _dataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_GetById",
                                                                       new { Id = orderId },
                                                                       _connectionString.SqlConnectionName);

            return recs.FirstOrDefault();
            //why do we use FirstOrDefault all the time?
            //cuz when you pass a unique identifier, you expect only one thing to come back.
            //So getting the first one usually works.
            //What if it doesn't exist? Return the default for objects, which is NULL. Nice, I learned something.
        }
    }
}

//Last step: Extract an interface from this class.