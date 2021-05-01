using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pooling;

namespace UI.InGame.Overtips
{
    public class EnemyOvertip : PooledMonoBehaviour
    {
        [SerializeField]
        private Image m_HealthBarImage;
        
        private EnemyData m_EnemyData;
        private Camera m_Camera;

        private void Awake()
        {
            m_Camera = Camera.main;
        }

        public void SetEnemyData(EnemyData data)
        {
            m_EnemyData = data;
        }

        public override void OnDestroyPooled()
        {
            m_EnemyData = null;
        }

        private void LateUpdate()
        {
            if (m_EnemyData == null)
            {
                return;
            }
            transform.position = m_Camera.WorldToScreenPoint(m_EnemyData.View.transform.position);
            m_HealthBarImage.fillAmount = .5f;
        }
    }
}