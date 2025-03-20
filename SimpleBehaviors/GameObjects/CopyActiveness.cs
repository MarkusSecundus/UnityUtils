using MarkusSecundus.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils
{
    public class CopyActiveness : MonoBehaviour
    {
        [SerializeField] GameObject Target;
        [SerializeField] bool Flip;

        private void OnEnable()
        {
            if (Target.IsNotNil()) Target.SetActive(!Flip);
        }
        private void OnDisable()
        {
            if (Target.IsNotNil()) Target.SetActive(Flip);
        }
    }
}
