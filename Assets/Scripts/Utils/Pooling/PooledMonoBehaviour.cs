using System;
using UnityEngine;

namespace Utils.Pooling
{
    public class PooledMonoBehaviour : MonoBehaviour
    {
        [NonSerialized]
        public PooledMonoBehaviour Prefab;

        private void Awake()
        {
            AwakePooled();
        }

        public virtual void AwakePooled()
        {
        }
    }
}