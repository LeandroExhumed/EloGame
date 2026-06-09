using UnityEngine;

namespace DefaultCompany.Utils.Pooling
{
    public interface IPool
    {
        Transform Container { get; }

        void AddPool (Object prefab, int size, Transform parent = null);
        T GetObject<T> (Object prefab) where T : Object;
    }
}