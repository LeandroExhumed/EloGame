using System.Collections.Generic;
using UnityEngine;

namespace DefaultCompany.Utils.Pooling
{
    public class Pool : IPool
    {
        public Transform Container
        {
            get
            {
                if (container == null)
                {
                    container = new GameObject("Pools").transform;
                }

                return container;
            }
        }

        private Dictionary<Object, Queue<Object>> pools = new Dictionary<Object, Queue<Object>>();

        private Transform container;


        public void AddPool (Object prefab, int size, Transform parent = null)
        {
            if (pools.ContainsKey(prefab))
            {
                return;
            }

            Queue<Object> queue = new Queue<Object>();

            for (int i = 0; i < size; ++i)
            {
                var o = Object.Instantiate(prefab, parent == null ? Container : parent);
                GameObject gameObject = GetGameObject(o);

                gameObject.gameObject.SetActive(false);

                queue.Enqueue(o);
            }

            pools[prefab] = queue;
        }

        public T GetObject<T> (Object prefab) where T : Object
        {
            Queue<Object> queue;
            if (pools.TryGetValue(prefab, out queue))
            {
                Object objectToReuse = queue.Dequeue();
                queue.Enqueue(objectToReuse);

                GameObject gameObject = GetGameObject(objectToReuse);
                gameObject.SetActive(true);

                if (gameObject.TryGetComponent(out IPoolable poolableObject))
                {
                    poolableObject.Reuse();
                }

                return objectToReuse as T;
            }

            UnityEngine.Debug.LogError("No pool was init with this prefab");
            return null;
        }

        private GameObject GetGameObject (Object instance)
        {
            GameObject gameObject = null;

            if (instance is Component component)
            {
                gameObject = component.gameObject;
            }
            else
            {
                gameObject = instance as GameObject;
            }

            return gameObject;
        }
    }
}