using UnityEngine;

using DrawShooter.Core.UnityExtensions;
using DrawShooter.Core.ArtificialIntelligence;

namespace DrawShooter.Core.Gameplay
{
    [RequireComponent(typeof(AutomaticPlayerMover))]
    public class NpcGroupChanger : ClassicNotCreatableMonoBehaviourSingletone<NpcGroupChanger>
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            ObservePlayerMovingEnd();
        }
        #endregion

        #region NpcGroupChanger
        private void ObservePlayerMovingEnd() 
        {
            AutomaticPlayerMover.GetInstance().OnPlayerMovingEnd += ChangeGroup;
        }

        private void ChangeGroup(NPCGroup newGroup) 
        {
            if (newGroup == null)
            {
                return;
            }

            System.Array.ForEach(newGroup.Npcs, (npc) => npc.GetComponent<NPCBrain>()?.StartMovingToPlayer());

            System.Array.ForEach(newGroup.Npcs, (npc) => npc.GetComponent<Sniper>()?.StartShootingAtPlayer());
        }
        #endregion
    }
}