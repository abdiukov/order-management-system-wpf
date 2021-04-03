using System;

namespace Domain
{
    public class InvalidOrderStateException : Exception
    {
        /// <summary>
        /// Invalid order state exception - did not end up needing it, as the program checks so that you cannot add a non-existing order state
        /// </summary>
        /// <param name="message"></param>
        public InvalidOrderStateException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

    }
}
