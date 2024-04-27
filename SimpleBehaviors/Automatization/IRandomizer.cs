using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.Automatization
{
    /// <summary>
    /// Component for randomizing a gameobject. Spawners like <see cref="PlaceObjectsOnPoints"/> invoke all components implementing this interface just after the spawn.
    /// </summary>
    public interface IRandomizer
    {
        /// <summary>
        /// Randomize the game object
        /// </summary>
        /// <param name="random">Randomness source</param>
        public void Randomize(System.Random random);

        public static void RandomizeAll(GameObject root, System.Random random, bool includeInactive=true)
        {
            foreach (var child in root.GetComponentsInChildren<IRandomizer>(includeInactive))
                child.Randomize(random);
        }
    }
}