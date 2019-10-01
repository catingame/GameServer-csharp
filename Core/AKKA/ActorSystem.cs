using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AKKA
{
    public class ActorSystem
    {
        private readonly Dictionary<Type, Dictionary<String, ActorRef>> _actorTypeDict;
        private readonly String _name;

        public ActorSystem(String Name)
        {
            this._name = Name;
            _actorTypeDict = new Dictionary<Type, Dictionary<string, ActorRef>>();
        }

        public static ActorSystem create(String name)
        {
            return new ActorSystem(name);
        }

        public ActorRef ActorOf(in Props props, String name)
        {
            // @TODO: exception handling
            if (HasActorRef(props.Type, name))
            {
                throw new Exception($"actor name [{name}] is not unique");
            }

            return AddActorRef(props, name);
        }

        private ActorRef AddActorRef(in Props props, String name)
        {
            var actorRef = new ActorRef(props, name);

            if (!_actorTypeDict.TryGetValue(props.Type, out var actorNameDict))
            {
                actorNameDict = new Dictionary<string, ActorRef> {{ name, actorRef}};
            }

            actorNameDict.TryAdd(name, actorRef);

            return actorRef;
        }

        private Boolean HasActorRef(Type type, String name)
        {
            if (_actorTypeDict.TryGetValue(type, out var actorNameDict))
            {
                return actorNameDict.ContainsKey(name);
            }

            return false;
        }
    }
}
