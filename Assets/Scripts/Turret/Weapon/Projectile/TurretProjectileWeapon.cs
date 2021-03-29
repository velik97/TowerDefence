﻿using System.Collections.Generic;
using Enemy;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class TurretProjectileWeapon : ITurretWeapon
    {
        private TurretProjectileWeaponAsset m_Asset;
        private TurretView m_View;

        private EnemyData m_ClosestEnemyData;
        private List<IProjectile> m_Projectiles = new List<IProjectile>();
        
        private float m_TimeBetweenShots;
        private float m_MaxDistance;

        private float m_LastShotTime = 0f;

        public TurretProjectileWeapon(TurretProjectileWeaponAsset asset, TurretView view)
        {
            m_Asset = asset;
            m_View = view;
            m_TimeBetweenShots = 1f / m_Asset.RateOfFire;
            m_MaxDistance = m_Asset.MaxDistance;
        }
        
        public void TickShoot()
        {
            TickWeapon();
            TickTower();
            TickProjectiles();
        }

        private void TickWeapon()
        {
            float passedTime = Time.time - m_LastShotTime;
            if (passedTime < m_TimeBetweenShots)
            {
                return;
            }

            m_ClosestEnemyData =
                Game.Player.EnemySearch.GetClosestEnemy(m_View.transform.position, m_MaxDistance);

            if (m_ClosestEnemyData == null)
            {
                return;
            }
            
            Shoot(m_ClosestEnemyData);
            m_LastShotTime = Time.time;
        }

        private void TickTower()
        {
            if (m_ClosestEnemyData != null)
            {
                m_View.TowerLookAt(m_ClosestEnemyData.View.transform.position);
            }
        }

        private void TickProjectiles()
        {
            for (int i = 0; i < m_Projectiles.Count; i++)
            {
                IProjectile projectile = m_Projectiles[i];
                projectile.TickProjectile();
                if (projectile.DidHit())
                {
                    projectile.DestroyProjectile();
                    m_Projectiles[i] = null;
                }
            }

            m_Projectiles.RemoveAll(p => p == null);
        }

        private void Shoot(EnemyData enemyData)
        {
            m_Projectiles.Add(m_Asset.ProjectileAsset.CreateProjectile(m_View.ProjectileOrigin.position, m_View.ProjectileOrigin.forward, enemyData));
        }
    }
}