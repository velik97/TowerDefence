using System.Collections.Generic;
using Enemy;
using JetBrains.Annotations;
using UnityEngine;

namespace Turret
{
    public class EnemySearch
    {
        private IReadOnlyList<EnemyData> m_EnemyDatas;

        public EnemySearch(IReadOnlyList<EnemyData> enemyDatas)
        {
            m_EnemyDatas = enemyDatas;
        }
        
        [CanBeNull]
        public EnemyData GetClosestEnemy(Vector3 center, float radius)
        {
            float maxSqrDistance = radius * radius;
            EnemyData closestEnemy = null;
            float closestSqrDistance = float.PositiveInfinity;
            
            foreach (EnemyData enemy in m_EnemyDatas)
            {
                float sqrDistance = (center - enemy.View.transform.position).sqrMagnitude;
                if (sqrDistance > maxSqrDistance)
                {
                    continue;
                }
                if (sqrDistance < closestSqrDistance)
                {
                    closestEnemy = enemy;
                    closestSqrDistance = sqrDistance;
                }
            }

            return closestEnemy;
        }
    }
}