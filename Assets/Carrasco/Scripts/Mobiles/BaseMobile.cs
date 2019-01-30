using System.Collections;
using System.Collections.Generic;
using Carrasco.Interfaces;
using UnityEngine;
using UnityEngine.AI;
namespace Carrasco.Mobiles
{
    public abstract class BaseMobile : MonoBehaviour, IPoolCallback
    {
        public float MaxHealth;
        public float Speed;

        [SerializeField]
        private float health;
        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value > this.MaxHealth ? this.MaxHealth : value;
            }
        }

        public abstract void Move();
        public abstract void Attack();
        public abstract void Die();

        public virtual void Update() {
            if (this.Health <= 0)
                {
                    this.Die();
                    return;
                }
        }

        public virtual void OnRecycleCallback()
        {
            this.Health = this.MaxHealth;
        }

        public virtual void OnSpawnCallback()
        {
            //throw new System.NotImplementedException();
        }
    }
}
