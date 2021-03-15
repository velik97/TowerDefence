using System;
using Enemy;

namespace Spawn
{
    [Serializable]
    public class SpawnWave
    {
        public EnemyAsset Enemy;
        public int EnemiesCount;
        public float TimeBetweenSpawns;

        public float TimeBeforeStartWave;
    }
}