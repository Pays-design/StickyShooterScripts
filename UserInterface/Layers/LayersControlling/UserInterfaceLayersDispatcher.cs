using System.Linq;

using DrawShooter.Core.UnityExtensions;

using Debug = UnityEngine.Debug;

namespace DrawShooter.Core.UserInterface.Layers
{
    public class UserInterfaceLayersDispatcher : ClassicMonoBehaviourSingletone<UserInterfaceLayersDispatcher>
    {
        #region Fields
        private UserInterfaceLayer[] m_layers;
        #endregion

        #region MonoBehaviour
        private void OnEnable()
        {
            LoadLayers();
        }

        private void OnDisable()
        {
            m_layers = null;
        }
        #endregion

        #region UserInterfaceLayersDispatcher
        private void LoadLayers() => m_layers = FindObjectsOfType<UserInterfaceLayer>(true);

        private void EnableLayer<TypeOfLayer>() where TypeOfLayer : UserInterfaceLayer
        {
            m_layers.ToList().ForEach(layer => layer.Disable());

            m_layers.OfType<TypeOfLayer>().First().Enable();
        }

        private void ReportAboutLayerEnablingImpossibility<TypeOfLayer>() where TypeOfLayer : UserInterfaceLayer
        {
            string nameOfType = typeof(TypeOfLayer).Name;

            Debug.LogError($"Cannot find layer: {nameOfType}. Or there more than one layer: {nameOfType}");
        }

        public void DisableLayers() 
        {
            System.Array.ForEach(m_layers, (layer) => layer.gameObject.SetActive(false));
        }

        public void TryEnableLayer<TypeOfLayer>() where TypeOfLayer : UserInterfaceLayer
        {            
            if (m_layers.OfType<TypeOfLayer>().Count() != 1)
            {
                ReportAboutLayerEnablingImpossibility<TypeOfLayer>();
            }
            else 
            {
                EnableLayer<TypeOfLayer>();
            }
        }
        #endregion
    }
}