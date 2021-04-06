using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Interfaces
{


    public interface IProcessWatcher
    {
        void StartWatch();

        event MyEnvetHandler ProcessTriggered;
    }
}
