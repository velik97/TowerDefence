using Enemy;
using Runtime;
using UnityEngine;

namespace Turret.Weapons.Projectiles
{
    public class TurretProjectileWeapon : ITurretWeapon
    {
        private TurretProjectileWeaponAsset m_Asset;
        private TurretView m_View;
        private float m_TimeBetweenShots;
        private float m_MaxShotDistance;

        private float m_LastShotTime;

        public TurretProjectileWeapon(TurretProjectileWeaponAsset asset, TurretView view)
        {
            m_Asset = asset;
            m_View = view;

            m_TimeBetweenShots = 1f / m_Asset.RateOfFire;
            m_LastShotTime = -m_TimeBetweenShots;

            m_MaxShotDistance = m_Asset.MaxShotDistance;
        }

        public void TickShoot()
        {
            float passedTime = Time.time - m_LastShotTime;
            if (passedTime < m_TimeBetweenShots)
            {
                return;
            }

            EnemyData closestEnemyData =
                Game.Player.EnemySearch.GetClosestEnemy(m_View.transform.position, m_MaxShotDistance);

            if (closestEnemyData == null)
            {
                return;
            }
            
            m_View.TowerLookAt(closestEnemyData.View.transform.position);
        }
    }
}