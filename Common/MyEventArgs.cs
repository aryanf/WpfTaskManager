using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MyEventArgs : EventArgs
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public ProcessAction Action { get; set; }
    }

    public delegate void MyEnvetHandler(object source, MyEventArgs args);
}
