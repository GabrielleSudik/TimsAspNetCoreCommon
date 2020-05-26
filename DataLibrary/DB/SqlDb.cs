using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DB
{
    public class SqlDb : IDataAccess
    {
        private readonly IConfiguration _config;
        //referencing the I instead of a concrete makes it more flexible.
        //all the constructor needs to know is it will get a Configuration.

        //"ctor" tabtab creates the basic constructor

        public SqlDb(IConfiguration config) //ctrl-dot for quick refactoring to add the using statement and the field.
        {
            //this.config = config;
            _config = config;
        }

        //use async to not block the UI while data loads.
        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            //that loads the connection string without hardcoding it.
            //so it can change at build or run time.

            //using means the connection will close properly
            //no matter what else happens like success or error etc.
            //how? SqlConnection class has a Dispose method,
            //and you set the connection to be an instance of SqlConnection.
            //Tim: ALWAYS CLOSE CONNECTIONS
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //1. Tim just knows what to type here. It's Dapper stuff.
                //2. Rt click a long list of args, refactor, "wrap argument list" to align them nicely.
                //3. What this does: sends stored proc, the params for it through Dapper
                //and it guards against Sql Inj. And tell it it's a stored proc (not a command like "CreateTable").
                //note we don't specify _which_ params the stored proc needs.
                //instead, you need to make sure that the list of params being passed around
                //matches what the stored proc will need.
                //4. We're fetching rows with Dapper instead of a DataTable with EF
                //because it will take the "T" -- the model -- and will easily match the data with the table columns.
                //Something like that.
                var rows = await connection.QueryAsync<T>(storedProcedure,
                                                          parameters,
                                                          commandType: CommandType.StoredProcedure);
                return rows.ToList();
            }
        }

        //Tim suggests you keep that last and next methods handy somewhere. You can use them a lot.
        //The next method starts similar.
        //We don't even _need_ an int returned in the next one,
        //but it's useful if you want to track what happened.

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(storedProcedure,
                                                     parameters,
                                                     commandType: CommandType.StoredProcedure);
            }
        }

        //Next step: Extract an Interface from the class.
        //go to class name, ctrl dot and "Extract Interface".
        //We renamed the default to IDataAccess.cs
    }
}
