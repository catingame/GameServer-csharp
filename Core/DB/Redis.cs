using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Core.DB
{
    class Redis : IDb
    {
        ConnectionMultiplexer _redis = null;
        IDatabase _db = null;
        Object _asyncObject = null;

        public Redis(String Address, Int32 Port, Boolean bAsync)
        {
            var dbAddress = $"{Address}:{Port}";
            _redis = ConnectionMultiplexer.Connect(dbAddress) 
                ?? throw new Exception($"Connection fail Db={dbAddress}");
            _asyncObject = bAsync ? new Object() : null;
        }

        public IDb Connect(DbOption Option)
        {
            return new Redis(Option.Address, Option.Port, Option.bAsync);
        }

        public Boolean SetDb(String Name)
        {
            if(Int32.TryParse(Name, out var DbID))
            { 
                _db = _redis.GetDatabase(DbID, _asyncObject);
                return true;
            }
            return false;
        }

        public String Get(String Key)
        {
            return _db?.StringGet(Key);
        }

        public void Set(String Key, String Value)
        {
            _db?.StringSet(Key, Value);
        }
    }
}
