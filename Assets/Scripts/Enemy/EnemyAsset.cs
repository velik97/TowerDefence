using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Assets/Enemy Asset", fileName = "Enemy Asset")]
    public class EnemyAsset : ScriptableObject
    {
        public float StartHealth;
        public float Speed;
        public int Damage;

        public EnemyView ViewPrefab;
    }
}