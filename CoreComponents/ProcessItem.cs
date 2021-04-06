using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreComponents
{
    public class ProcessItem : IProcessItem
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }
}
