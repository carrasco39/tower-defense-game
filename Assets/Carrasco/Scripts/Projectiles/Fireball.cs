using Carrasco.Extensions;
using Carrasco.Mobiles;

namespace Carrasco.Projectiles {
    public class Fireball : BaseProjectile
    {
        public override void OnHit(BaseMobile mobile)
        {
            mobile.Health -= this.Damage;
            this.gameObject.Recycle(this);
        }

        public override void OnRecycleCallback()
        {
            
        }

        public override void OnSpawnCallback()
        {
            
        }
    }
}