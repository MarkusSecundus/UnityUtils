using MarkusSecundus.Utils.Behaviors.Automatization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Assets.Scripts.Utils.SimpleBehaviors.Automatization
{
    public class RandomizeOnStartup : MonoBehaviour
    {
        [SerializeField] int Seed = -1;
        [SerializeField] bool IncludeInactive = true;

        private void Start()
        {
            System.Random rand = Seed == -1 ? new System.Random() : new System.Random(Seed);
            IRandomizer.RandomizeAll(gameObject, rand, IncludeInactive);
        }
    }
}
