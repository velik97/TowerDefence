using Runtime;

namespace Field
{
    public class GirdPointerController : IController
    {
        private GridHolder m_GridHolder;

        public GirdPointerController(GridHolder gridHolder)
        {
            m_GridHolder = gridHolder;
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            m_GridHolder.RayCastInGrid();
        }
    }
}