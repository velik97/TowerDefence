using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        private TurretData m_Data;

        public TurretData Data => m_Data;

        public void AttachData(TurretData data)
        {
            m_Data = data;
            transform.position = data.Node.Position;
        }
    }
}