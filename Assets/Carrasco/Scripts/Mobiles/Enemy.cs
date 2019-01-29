using UnityEngine;
using UnityEngine.AI;
using Carrasco.Extensions;
namespace Carrasco.Mobiles
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : BaseMobile
    {
        NavMeshAgent agent;
        Animator animator;
        public void Start()
        {
            this.agent = GetComponent<NavMeshAgent>();
            this.animator = GetComponent<Animator>();
            this.agent.speed = this.Speed;
            this.Move();
        }

        public void Update()
        {
            if (this.agent)
            {
                this.animator.SetFloat("Speed", this.agent.velocity.sqrMagnitude);
            }
        }


        public override void Attack() { }

        public override void Move()
        {
            var path = GameObject.Find("Reach").transform.position;
            this.agent.SetDestination(path);
        }
    }
}