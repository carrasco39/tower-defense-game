using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Carrasco.Mobiles
{
    public abstract class BaseMobile : MonoBehaviour
    {
        public float MaxHealth;

        public abstract void Move();
    }
}
