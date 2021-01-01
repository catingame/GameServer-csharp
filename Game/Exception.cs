using System;

namespace SimpleGame
{
    public class InLodingGameException : Exception
    {
        public enum EErrorMessage
        {
            NONE,
            INVALID_PORT
        }

        public InLodingGameException()
        {
        }

        public InLodingGameException(EErrorMessage message)
            : base(message.ToString())
        {
        }

        public InLodingGameException(EErrorMessage message, Exception inner)
            : base(message.ToString(), inner)
        {
        }
    }
}
