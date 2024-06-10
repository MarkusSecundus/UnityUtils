using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusSecundus.Utils.Assets._Scripts.Utils.SimpleBehaviors.Visuals
{
    public interface IStoppable
    {
        public bool IsRunning { get; }
        public void Stop();
    }
}
