using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        private float m_Speed;

        private Transform m_Transform;
        private Node m_TargetNode;
        
        private const float TOLERANCE = 0.1f;

        public GridMovementAgent(float speed, Transform transform, Grid grid)
        {
            m_Speed = speed;
            m_Transform = transform;

            m_Transform.position = grid.GetStartNode().Position;
            SetStartNode(grid.GetStartNode());
        }

        public void TickMoving()
        {
            if (m_TargetNode == null)
            {
                return;
            }

            Vector3 target = m_TargetNode.Position;
            
            float distance = (target - m_Transform.position).magnitude;
            if (distance < TOLERANCE)
            {
                m_TargetNode = m_TargetNode.NextNode;
                return;
            }
        
            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            m_Transform.Translate(delta);
        }

        private void SetStartNode(Node node)
        {
            m_TargetNode = node;
        }
    }
}