using Enemy;
using UnityEngine;

namespace Turret.Weapons.Projectiles
{
    public abstract class ProjectileAssetBase : ScriptableObject
    {
        public abstract MonoBehaviour CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData);
    }
}