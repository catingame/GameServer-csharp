using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB
{
    public interface IDb
    {
        IDb Connect(DbOption Option);
        Boolean SetDb(String Name);
        String Get(String Key);
        void Set(String Key, String Value);
    }

    public class DbOption
    {
        public String Address;
        public Int32 Port;
        public Boolean bAsync;
    }
}
