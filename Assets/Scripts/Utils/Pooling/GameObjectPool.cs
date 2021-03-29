using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pooling
{
    public class GameObjectPool : MonoBehaviour
    {
        private static GameObjectPool s_Instance;
        private Dictionary<int, Queue<MonoBehaviour>> m_PooledObjects;

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
            where TMonoBehaviour : MonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, position, rotation, parent);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position,
            Quaternion rotation)
            where TMonoBehaviour : MonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, position, rotation);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Transform parent)
            where TMonoBehaviour : MonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour, parent);
        }
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour monoBehaviour)
            where TMonoBehaviour : MonoBehaviour
        {
            return Instance.InstantiatePooledImpl(monoBehaviour);
        }

        public static void ReturnPooledObject(MonoBehaviour monoBehaviour)
        {
            Instance.ReturnPooledObjectImpl(monoBehaviour);
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position, Quaternion rotation, Transform parent)
            where TMonoBehaviour : MonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            Transform instanceTransform = instance.transform;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
            instanceTransform.parent = parent;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Vector3 position, Quaternion rotation)
            where TMonoBehaviour : MonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            Transform instanceTransform = instance.transform;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour, Transform parent)
            where TMonoBehaviour : MonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(monoBehaviour);
            instance.transform.parent = parent;
            return instance;
        }

        private TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour monoBehaviour)
            where TMonoBehaviour : MonoBehaviour
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
                }
            }

            if (instance == null)
            {
                instance = Instantiate(monoBehaviour);
            }

            return instance;
        }

        private void ReturnPooledObjectImpl(MonoBehaviour monoBehaviour)
        {
            int id = monoBehaviour.GetInstanceID();
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