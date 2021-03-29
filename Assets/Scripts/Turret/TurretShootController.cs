﻿using Runtime;

namespace Turret
{
    public class TurretShootController : IController
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