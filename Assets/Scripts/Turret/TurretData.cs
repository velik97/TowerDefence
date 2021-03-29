using Field;
using Turret.Weapon;
using Turret.Weapon.Projectile;

namespace Turret
{
    public class TurretData
    {
        private TurretAsset m_Asset;
        
        private TurretView m_View;
        private ITurretWeapon m_Weapon;
        private Node m_Node;

        public TurretView View => m_View;
        public Node Node => m_Node;
        public ITurretWeapon Weapon => m_Weapon;

        public TurretData(TurretAsset asset, Node node)
        {
            m_Asset = asset;
            m_Node = node;
        }

        public void AttachView(TurretView view)
        {
            m_View = view;
            m_View.AttachData(this);

            m_Weapon = m_Asset.WeaponAsset.GetWeapon(view);
        }
    }
}