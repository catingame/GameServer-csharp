using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AKKA
{
    public class Props
    {
        public Type Type { get; }

        public Props(Type @Type)
        {
            this.Type = @Type;
        }

        public static Props create(Type @Type )
        {
            return new Props(@Type);
        }
    }
}
