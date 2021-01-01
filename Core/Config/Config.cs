using System;
using System.Collections.Generic;

namespace Core.Config
{
    public abstract class Config
    {
        Dictionary<String, Object> _data;

        public Config()
        {
            Setup();
        }
        
        public Boolean Set<TType>(String key, TType value)
        {
            return _data.TryAdd(key, value);
        }

        public Boolean Get<TType>(String key, out TType value)
        {
            if (_data.TryGetValue(key, out var _value))
            {
                value = (TType)_value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        protected abstract void Setup();
    }
}
