namespace Turret.Weapon.Projectile
{
    public interface IProjectile
    {
        void TickProjectile();
        bool DidHit();
        void DestroyProjectile();
    }
}