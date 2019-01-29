using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Carrasco.Mobiles
{
    public abstract class BaseMobile : MonoBehaviour
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
    }
}
