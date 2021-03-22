using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Tower;
        
        private TurretData m_Data;

        public TurretData Data => m_Data;

        public void AttachData(TurretData data)
        {
            m_Data = data;
            transform.position = data.Node.Position;
        }

        public void TowerLookAt(Vector3 target)
        {
            m_Tower.LookAt(target, Vector3.up);
        }
    }
}