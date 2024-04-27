using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace MarkusSecundus.Utils.Extensions
{
    public static class NavMeshHelpers
    {
        public static float GetRemainingDistanceUntilStop(this NavMeshAgent self) => Mathf.Max(0f, self.remainingDistance - self.stoppingDistance);
    }
}
