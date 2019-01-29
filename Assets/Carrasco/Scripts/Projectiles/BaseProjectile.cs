using UnityEngine;
using Carrasco.Mobiles;
using Carrasco.Interfaces;
using Carrasco.Extensions;
namespace Carrasco.Projectiles
{
    //[RequireComponent(typeof(Rigidbody))]
    public abstract class BaseProjectile : MonoBehaviour, IPoolCallback
    {
        public float Damage;
        public float TTL;

        async void Start()
        {
            await new WaitForSeconds(TTL);
            this.gameObject.Recycle(this);
        }
        public abstract void OnHit(BaseMobile mobile);

        // 

        public abstract void OnRecycleCallback();
        public abstract void OnSpawnCallback();

        public virtual void Update()
        {
            this.transform.position += this.transform.forward * 20f * Time.deltaTime;


            var layerMask = LayerMask.GetMask("Mobiles");
            var cols = Physics.OverlapSphere(this.transform.position, 1, layerMask);
            if (cols.Length > 0)
            {
                var mobile = cols[0].GetComponent<BaseMobile>();
                if(mobile) this.OnHit(mobile);
            }
        }
    }
}