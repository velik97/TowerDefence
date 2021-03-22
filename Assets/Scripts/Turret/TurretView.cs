using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Tower;

        [SerializeField]
        private Transform m_ProjectilePivot;
        
        private TurretData m_Data;

        public TurretData Data => m_Data;

        public Transform ProjectilePivot => m_ProjectilePivot;

        public void AttachData(TurretData data)
        {
            m_Data = data;
            transform.position = data.Node.Position;
        }

        public void TowerLookAt(Vector3 target)
        {
            target.y = m_Tower.position.y;
            m_Tower.LookAt(target, Vector3.up);
        }
    }
}