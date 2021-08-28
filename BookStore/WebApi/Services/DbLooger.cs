using System;

namespace WebApi.Services
{
    public class DbLooger:ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DbLooger] "+message);
        }
    }
}