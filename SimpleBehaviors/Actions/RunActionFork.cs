using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils
{
    public class RunActionFork : MonoBehaviour
    {
        [SerializeField] UnityEvent IfTrue;
        [SerializeField] UnityEvent IfFalse;

        public void Invoke(bool condition) => (condition ? IfTrue: IfFalse)?.Invoke();
    }
}
