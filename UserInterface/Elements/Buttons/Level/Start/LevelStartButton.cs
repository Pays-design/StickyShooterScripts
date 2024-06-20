using DrawShooter.Core.UserInterface.Layers;
using DrawShooter.Core.Gameplay;

using MonoBehaviour = UnityEngine.MonoBehaviour;
using IPointerDownHandler = UnityEngine.EventSystems.IPointerDownHandler;
using PointerEventData = UnityEngine.EventSystems.PointerEventData;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class LevelStartButton : MonoBehaviour, IPointerDownHandler
    {
        #region IPointerDownHandler
        public void OnPointerDown(PointerEventData eventData)
        {
            StartLevel();
        }
        #endregion

        #region LevelStartButton
        private void StartLevel() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<GameplayLayer>();

            NPCGroupDeathObserver.GetInstance().StartObserving();
        }
        #endregion
    }
}