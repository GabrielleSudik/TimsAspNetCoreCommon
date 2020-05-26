using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.DB
{
    interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}

//We made IDataAccess to when other stuff gets injections,
//they get the "top level" IDataAccess contract, and not the specific
//concrete versions. So you can have multiple implementing classes,
//just make sure the code that needs it references the correct concrete class.
//Tim notes it is very rare for code to rely on diff types of DBs
//but this approach is useful with unit tests, where the test data
//might not even come from a DB. So the flexibility to connecting to that data
//is already there.
