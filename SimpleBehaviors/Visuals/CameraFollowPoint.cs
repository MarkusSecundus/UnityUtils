using MarkusSecundus.Utils.Extensions;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.Cosmetics
{
    /// <summary>
    /// Synchronizes transform of a <see cref="Camera"/> object with another transform each frame.
    /// </summary>
    public class CameraFollowPoint : MonoBehaviour
    {
        /// <summary>
        /// Transform to follow
        /// </summary>
        public Transform Target;
        private void LateUpdate()
        {
            if (Target.IsNotNil())
            {
                (transform.position, transform.rotation, transform.localScale) = (Target.position, Target.rotation, Target.localScale);
            }
        }
    }
}