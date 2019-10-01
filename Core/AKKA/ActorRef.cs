using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AKKA
{
    public class ActorRef
    {
        private readonly String _name;

        public ActorRef(Props props, String name)
        {
            this._name = name;
        }

        public static ActorRef noSender()
        {
            // @TODO:
            return null;
        }

        public void Tell(in Object message, in ActorRef sender)
        {
        }

        public void Ask()
        {
        }
    }
}
