using UnityEngine;
using UnityEngine.AI;
using Carrasco.Extensions;
using Carrasco.Interfaces;
namespace Carrasco.Pleaceables
{
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Barrier : BasePlaceable
    {

        public float LifeTime = 10; // default 10 seconds
        private float currLifeTime;
        public NavMeshObstacle obstacle;

        public override void Start()
        {
            base.Start();
            this.obstacle = GetComponent<NavMeshObstacle>();
            this.obstacle.enabled = false;
        }

        void Update()
        {
            if (this.IsPlaced)
            {
                if (this.currLifeTime > this.LifeTime)
                {
                    this.gameObject.Recycle(this);
                    print("Recycling");

                }
                this.currLifeTime += Time.deltaTime;
            } 
        }
        public override void ConfirmPlace()
        {
            base.ConfirmPlace();
            this.obstacle.enabled = true;
        }

        public override void OnRecycleCallback()
        {
            base.OnRecycleCallback();
            this.currLifeTime = 0;
            this.obstacle.enabled = false;
        }
    }
}