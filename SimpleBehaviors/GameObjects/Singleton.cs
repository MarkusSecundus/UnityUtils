using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.GameObjects
{
    public class Singleton<TSelf> : MonoBehaviour where TSelf : Singleton<TSelf>
    {
        private static TSelf _instance;
        private static object _instanceLock = new object();

        protected virtual bool _shouldNotDestroyOnLoad => true;

        public static TSelf Instance { get
            {
                if (_instance) return _instance;
                lock (_instanceLock)
                {
                    if (_instance) return _instance;
                    _instance = GameObject.FindFirstObjectByType<TSelf>();
                    if (_instance) return _instance;

                    _instance = new GameObject($"({nameof(Singleton<TSelf>)}){typeof(TSelf)}").AddComponent<TSelf>();
                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            lock (_instanceLock)
            {
                if (!_instance)
                    _instance = (TSelf)this;
                else if (_instance != this)
                    Destroy(gameObject);
            }
            if (this._shouldNotDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

    }
}
