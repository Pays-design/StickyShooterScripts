using UnityEngine;

using DrawShooter.Core.UserInterface.Layers;

using LevelFinish = DrawShooter.Core.Gameplay.LevelFinish;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class LevelFinishStarter : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            ObserveFinishTriggerEnter();
        }
        #endregion

        #region LevelFinishStarter
        private void ObserveFinishTriggerEnter() 
        {
            LevelFinish levelFinish = FindObjectOfType<LevelFinish>();

            levelFinish.OnPlayerEnter += EnableFinishLayer;
        }

        private void EnableFinishLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<FinishLayer>();

            System.Array.ForEach(FindObjectsOfType<ArtificialIntelligence.NPCBrain>(true), (npcBrain) => npcBrain.StopMovingToPlayer());
        }
        #endregion
    }
}