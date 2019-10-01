using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AKKA
{
    public abstract class UntypedActor
    {
        public UntypedActor(Context ctx, Type baseClass)
        {
            if (ctx?.IsValid() ?? true)
            {
                throw new Exception($" You cannot create an instance of {baseClass} explicitly using the constructor(new).");
            }


        }

        public abstract void OnReceive(Object message);

    } // Native AKKA Actor Class
}
