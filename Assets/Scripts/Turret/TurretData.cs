namespace Turret
{
    public class TurretData
    {
        private TurretView m_View;

        public TurretView View => m_View;

        public TurretData(TurretAsset asset)
        {
            
        }

        public void AttachView(TurretView view)
        {
            m_View = view;
            m_View.AttachData(this);
        }
    }
}