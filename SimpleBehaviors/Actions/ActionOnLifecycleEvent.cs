using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils
{
    public class ActionOnLifecycleEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent _onStart;
        [SerializeField] UnityEvent _onAwake;
        [SerializeField] UnityEvent _onDestroy;


        void Start() => _onStart?.Invoke();
        private void Awake() => _onAwake?.Invoke();
        private void OnDestroy() => _onDestroy?.Invoke();
    }
}
