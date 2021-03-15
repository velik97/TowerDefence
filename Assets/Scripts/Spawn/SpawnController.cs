using Enemy;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Spawn
{
    public class SpawnController : IController
    {
        private SpawnWavesAsset m_SpawnWaves;
        private Grid m_Grid;
        
        private float m_SpawnStartTime;
        private float m_PassedTimeAtPreviousFrame = -1f;

        public SpawnController(SpawnWavesAsset spawnWaves, Grid grid)
        {
            m_SpawnWaves = spawnWaves;
            m_Grid = grid;
        }

        public void OnStart()
        {
            m_SpawnStartTime = Time.time;
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            float passedTime = Time.time - m_SpawnStartTime;
            float timeToSpawn = 0f;

            foreach (SpawnWave wave in m_SpawnWaves.SpawnWaves)
            {
                timeToSpawn += wave.TimeBeforeStartWave;

                for (int i = 0; i < wave.EnemiesCount; i++)
                {
                    if (passedTime >= timeToSpawn && m_PassedTimeAtPreviousFrame < timeToSpawn)
                    {
                        SpawnEnemy(wave.Enemy);
                        m_PassedTimeAtPreviousFrame = passedTime;
                        return;
                    }

                    if (i < wave.EnemiesCount - 1)
                    {
                        timeToSpawn += wave.TimeBetweenSpawns;
                    }
                }
            }
        }

        private void SpawnEnemy(EnemyAsset enemyAsset)
        {
            EnemyView view = Object.Instantiate(enemyAsset.ViewPrefab);
            view.transform.position = m_Grid.GetStartNode().Position;
            EnemyData data = new EnemyData(enemyAsset);

            data.AttachView(view);
            view.StartMovementAgent(m_Grid);

            Game.Player.EnemySpawned(data);
        }
    }
}