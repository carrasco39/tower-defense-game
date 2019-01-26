using Carrasco.Core;
using Carrasco.Interfaces;
using UnityEngine;
namespace Carrasco.Extensions
{
    public static class PoolExtensions
    {
        public static void Recycle(this GameObject go, IPoolCallback callback) {
            GameObjectPool.Recycle(go);
            callback.OnRecycleCallback();
        }

        public static GameObject Spawn(this GameObject go, IPoolCallback callback) {
            callback.OnSpawnCallback();
            return GameObjectPool.Spawn(go);
        }
    } 
}