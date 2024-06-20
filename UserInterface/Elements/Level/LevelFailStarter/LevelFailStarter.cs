using UnityEngine;

using DrawShooter.Core.Gameplay;
using DrawShooter.Core.UserInterface.Layers;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class LevelFailStarter : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            ObservePlayerDeath();
        }
        #endregion

        #region LevelFailStarter
        private void ObservePlayerDeath() 
        {
            Player player = FindObjectOfType<Player>();

            player.OnStun += DisableGameplayLayer;

            player.OnDie += EnableLevelFailLayer;
        }

        private void EnableLevelFailLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<LevelFailLayer>();
        }

        private void DisableGameplayLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().DisableLayers();
        }
        #endregion
    }
}