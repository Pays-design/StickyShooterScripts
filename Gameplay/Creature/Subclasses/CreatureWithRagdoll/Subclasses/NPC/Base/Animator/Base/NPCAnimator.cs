using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

using NPCBrain = DrawShooter.Core.ArtificialIntelligence.NPCBrain;

namespace DrawShooter.Core.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class NPCAnimator : MonoBehaviour
    {
        #region Fields
        private Animator m_animator;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            NPC npc = GetComponent<NPC>();

            ObserveNPCDeath(npc);

            ObservePlayerKillByNPC(npc);

            ObserveNPCBrain();

            m_animator = GetComponent<Animator>();
        }
        #endregion

        #region NPCAnimator
        private IEnumerator MakeNPCTied() 
        {
            m_animator.SetBool("IsTied", true);

            yield return new WaitForEndOfFrame();

            Destroy(this);

            m_animator.enabled = false;
        }

        private void DisableAnimator() 
        {
            StartCoroutine(MakeNPCTied());  
        }

        private void EnablePlayerKillAnimation()
        {
            m_animator.SetBool("IsWalking", false);

            m_animator.SetBool("IsAttacking", true);
        }

        private void ObserveNPCBrain() 
        {
            NPCBrain npcBrain = GetComponent<NPCBrain>();

            if (npcBrain == null)
                return;

            npcBrain.OnWalkingEnd += () => m_animator.SetBool("IsWalking", false);

            npcBrain.OnWalkingStart += () => m_animator.SetBool("IsWalking", true);
        }

        private void ObserveNPCDeath(NPC npc) 
        {
            npc.OnDie += DisableAnimator;
        }

        private void ObservePlayerKillByNPC(NPC npc) 
        {
            npc.OnPlayerKill += EnablePlayerKillAnimation;
        }
        #endregion
    }
}