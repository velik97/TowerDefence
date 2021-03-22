using Runtime;

namespace Turret.Weapons
{
    public class TurretWeaponController : IController
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            foreach (TurretData turretData in Game.Player.TurretDatas)
            {
                turretData.Weapon.TickShoot();
            }
        }
    }
}