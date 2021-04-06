using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace TestCore.Mocks
{
    class MockSimpleLogger : ISimpleLogger
    {
        public void Log(string logContent)
        {
            throw new NotImplementedException();
        }
    }
}
