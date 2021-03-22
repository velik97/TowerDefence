using Field;
using Turret.Weapons;

namespace Turret
{
    public class TurretData
    {
        private Node m_Node;
        private TurretView m_View;
        private TurretAsset m_Asset;
        private ITurretWeapon m_Weapon;

        public TurretView View => m_View;

        public Node Node => m_Node;


        public ITurretWeapon Weapon => m_Weapon;

        public TurretData(TurretAsset asset, Node node)
        {
            m_Node = node;
            m_Asset = asset;
        }

        public void AttachView(TurretView view)
        {
            m_View = view;
            m_View.AttachData(this);

            m_Weapon = m_Asset.WeaponAsset.GetWeapon(m_View);
        }
    }
}