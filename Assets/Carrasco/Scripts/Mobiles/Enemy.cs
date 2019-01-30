using UnityEngine;
using UnityEngine.AI;
using Carrasco.Extensions;
using Carrasco.Core;

namespace Carrasco.Mobiles
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : BaseMobile
    {
        public NavMeshAgent agent;
        Animator animator;

        public float KillReward;

        public void Start()
        {
            this.agent = GetComponent<NavMeshAgent>();
            this.animator = GetComponent<Animator>();
            this.agent.enabled = false;
            this.agent.speed = this.Speed;
        }

        public override void Update()
        {
            base.Update();
            if (this.agent)
            {
                this.animator.SetFloat("Speed", this.agent.velocity.sqrMagnitude);
            }

            var cols = Physics.OverlapSphere(this.transform.position, 0.5f);
            foreach(var col in cols) {
                if(col.name == "Reach") {
                    this.gameObject.Recycle(this);
                    GameManager.Instance.Life--;
                }
            }
        }


        public override void Attack() { }

        public async override void Move()
        {
            var path = GameObject.Find("Reach").transform.position;
            await new WaitUntil(() => this.agent.enabled);
            this.agent.SetDestination(path);
        }

        public override void Die()
        {
            this.gameObject.Recycle(this);
            GameManager.Instance.Score += KillReward;
        }

        public override void OnRecycleCallback() {
            base.OnRecycleCallback();
            this.agent.enabled = false;
        }
    }
}