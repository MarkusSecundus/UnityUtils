using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils.Behaviors.Actions
{
    public class ActionOnChildrenCount : MonoBehaviour
    {
        [SerializeField] int MinChildrenInclusive;
        [SerializeField] int MaxChildrenInclusive;
        [SerializeField] UnityEvent OnChildrenInInterval;
        private void Update()
        {
            if (transform.childCount >= MinChildrenInclusive && transform.childCount <= MaxChildrenInclusive)
                OnChildrenInInterval?.Invoke();
        }
    }
}
