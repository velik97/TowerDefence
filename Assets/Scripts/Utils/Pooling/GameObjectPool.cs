using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pooling
{
    public class GameObjectPool : MonoBehaviour
    {
        private static GameObjectPool s_Instance;
        private Dictionary<int, Queue<MonoBehaviour>> m_PooledObjects = new Dictionary<int, Queue<MonoBehaviour>>();

        private static GameObjectPool Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<GameObjectPool>();
                    if (s_Instance == null)
                    {
                        GameObject gameObj = new GameObject("GameObjectPool");
                        s_Instance = gameObj.AddComponent<GameObjectPool>();
                    }

                    s_Instance.gameObject.SetActive(false);
                }
                return s_Instance;
            }
        }

        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position,
            Quaternion rotation, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, position, rotation, parent);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position,
            Quaternion rotation)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, position, rotation);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, parent);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = Instance.InstantiatePooledImpl(monoBehaviour);
            instance.transform.parent = null;
            return instance;
        }

        public static void ReturnPooledObject(PooledMonoBehaviour monoBehaviour)
        {
            Instance.ReturnPooledObjectImpl(monoBehaviour);
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position, Quaternion rotation, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            Transform instanceTransform = instance.transform;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
            instanceTransform.parent = parent;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position, Quaternion rotation)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            Transform instanceTransform = instance.transform;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
            instanceTransform.parent = null;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            instance.transform.parent = parent;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            int id = monoBehaviour.GetInstanceID();
            TMonoBehaviour instance = null;
            if (m_PooledObjects.TryGetValue(id, out Queue<MonoBehaviour> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Peek() as TMonoBehaviour;
                    if (instance == null)
                    {
                        throw new InvalidCastException($"Cannot cast {queue.Peek()} to {typeof(TMonoBehaviour)}");
                    }
                    queue.Dequeue();
                    instance.AwakePooled();
                }
            }

            if (instance == null)
            {
                instance = Instantiate(monoBehaviour);
                instance.Prefab = monoBehaviour;
            }

            return instance;
        }

        private void ReturnPooledObjectImpl(PooledMonoBehaviour monoBehaviour)
        {
            int id = monoBehaviour.Prefab.GetInstanceID();
            if (m_PooledObjects.TryGetValue(id, out Queue<MonoBehaviour> queue))
            {
                queue.Enqueue(monoBehaviour);
            }
            else
            {
                Queue<MonoBehaviour> newQueue = new Queue<MonoBehaviour>();
                newQueue.Enqueue(monoBehaviour);
                m_PooledObjects[id] = newQueue;
            }

            monoBehaviour.transform.parent = transform;
        }
    }
}