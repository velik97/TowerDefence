﻿using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        public void AttachData(EnemyData data)
        {
            m_Data = data;
        }

        public void StartMovementAgent(Grid grid)
        {
            m_MovementAgent = new GridMovementAgent(5f, transform, grid);
        }
    }
}