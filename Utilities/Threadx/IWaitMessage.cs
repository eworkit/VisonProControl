using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public interface IWaitMessage
    {
        string Message { get; set; }
        void Dispose();
    }
}
