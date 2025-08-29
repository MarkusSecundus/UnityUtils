using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils
{
    public class SetFpsCap : MonoBehaviour
    {
        [SerializeField] int FpsCap = 120;
        private void Start()
        {
            Application.targetFrameRate = FpsCap;
        }
    }
}
