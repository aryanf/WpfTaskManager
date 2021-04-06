using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace CoreComponents
{
    // This should be replaced with actual logger and implement more meaningful interface like Microsoft.Extensions.Logging
    public class MySimpleLogger : ISimpleLogger
    {
        public void Log(string logContent)
        {
            Console.WriteLine(logContent);
        }
    }
}
