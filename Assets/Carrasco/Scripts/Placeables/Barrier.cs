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

        public override void OnRecycleCallback()
        {
            base.OnRecycleCallback();
            this.currLifeTime = 0;
        }
    }
}