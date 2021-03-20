using EnemySpawn;
using TurretSpawn;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Level Asset", fileName = "Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public TurretMarketAsset TurretMarketAsset;
        public SpawnWavesAsset SpawnWavesAsset;
    }
}