using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pooling;

namespace UI.InGame.Overtips
{
    public class EnemyOvertip : PooledMonoBehaviour
    {
        [SerializeField]
        private RectTransform m_HealthBar;
        private Camera m_Camera;

        private EnemyData m_EnemyData;
        private Transform m_EnemyTransform;
        private float m_MaxHealth;

        private void Awake()
        {
            m_Camera = Camera.main;
        }

        public void SetEnemyData(EnemyData data)
        {
            m_EnemyData = data;
            m_MaxHealth = m_EnemyData.Asset.StartHealth;
            m_EnemyTransform = m_EnemyData.View.Center;
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
            transform.position = m_Camera.WorldToScreenPoint(m_EnemyTransform.position);
            SetHealth(m_EnemyData.Health / m_MaxHealth);
        }

        private void SetHealth(float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            
            m_HealthBar.anchorMin = Vector2.zero;
            m_HealthBar.anchorMax = new Vector2(percentage, 1f);
        }
    }
}