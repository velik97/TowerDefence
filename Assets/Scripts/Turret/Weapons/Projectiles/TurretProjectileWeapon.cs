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

        private EnemyData m_LastEnemyData;

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
            if (m_LastEnemyData != null)
            {
                m_View.TowerLookAt(m_LastEnemyData.View.transform.position);
            }
            
            float passedTime = Time.time - m_LastShotTime;
            if (passedTime < m_TimeBetweenShots)
            {
                return;
            }

            m_LastShotTime = Time.time;
            m_LastEnemyData = Game.Player.EnemySearch.GetClosestEnemy(m_View.transform.position, m_MaxShotDistance);

            if (m_LastEnemyData != null)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            m_Asset.ProjectileAsset.CreateProjectile(m_View.ProjectilePivot.position, m_View.ProjectilePivot.forward,
                m_LastEnemyData);
        }
    }
}