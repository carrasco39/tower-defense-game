using System;
using System.Linq;
using Carrasco.Extensions;
using Carrasco.Interfaces;
using Carrasco.Mobiles;
using Carrasco.Projectiles;
using UnityEngine;

namespace Carrasco.Pleaceables
{
    public class Tower : BasePlaceable
    {
        enum ETowerState
        {
            WATCHING,
            ATTACKING,
            LOADING
        }

        [SerializeField]
        private float attack;
        [SerializeField]
        private float attackDelay;
        [SerializeField]
        private float attackRange;

        [SerializeField]
        private BaseProjectile projectile;

        private ETowerState state;
        private BaseMobile target;

        public override void Start()
        {
            base.Start();
            this.state = ETowerState.WATCHING;
            if(!this.projectile) this.projectile = Resources.Load<Fireball>("Fireball");
            this.projectile.Damage = this.attack;
        }

        void Update()
        {
            if(!this.IsPlaced) return;
            if(this.state == ETowerState.LOADING) return;
            if (this.state == ETowerState.WATCHING)
            {
                var layerMask = LayerMask.GetMask("Mobiles");
                var cols = Physics.OverlapSphere(this.transform.position, this.attackRange, layerMask);
                if (cols.Length > 0)
                {
                    var target = cols[0].GetComponent<BaseMobile>();
                    Debug.Log(target.gameObject.name);
                    if (target)
                    {
                        Debug.Log("ATTACKING!!!!");
                        this.target = target;
                        this.state = ETowerState.ATTACKING;
                    }
                    
                }
                return;
            }

            if (target && Vector3.Distance(this.transform.position, target.transform.position) > this.attackRange)
            {
                this.target = null;
                this.state = ETowerState.WATCHING;
                return;
            }

            if (this.target && this.state == ETowerState.ATTACKING)
            {
                this.Attack();
            }

        }

        private async void Attack()
        {
            this.projectile.gameObject.SetActive(false);
            var go = this.projectile.gameObject.Spawn(this.projectile);
            go.transform.position = this.transform.position + Vector3.up * 2;
            go.SetActive(true);
            go.transform.rotation = Quaternion.identity;
            go.transform.LookAt(this.target.transform);
            this.state = ETowerState.LOADING;
            await new WaitForSeconds(this.attackDelay);
            this.state = ETowerState.ATTACKING;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.localPosition, this.attackRange);
        }
    }
}