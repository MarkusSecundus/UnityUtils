using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.Physics
{
    /// <summary>
    /// Component that disables collisions of <c>this</c> gameobject with specified colliders on startup and then destroys itself.
    /// </summary>
    public class CollisionDisabler : MonoBehaviour
    {
        /// <summary>
        /// Colliders that should no longer collide with <c>this</c> gameobject
        /// </summary>
        public Collider[] ToDisable;


        // Start is called before the first frame update
        void Start()
        {
            var localColliders = GetComponentsInChildren<Collider>();
            foreach (var local in localColliders)
                foreach (var other in ToDisable)
                    UnityEngine.Physics.IgnoreCollision(local, other);
            Destroy(this);
        }
    }
}