using UnityEngine;
using UnityEngine.AI;

using IEnumerator = System.Collections.IEnumerator;
using Action = System.Action;

using Player = DrawShooter.Core.Gameplay.Player;
using NPC = DrawShooter.Core.Gameplay.NPC;

namespace DrawShooter.Core.ArtificialIntelligence
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(NPC))]
    public class NPCBrain : MonoBehaviour
    {
        #region SerializeFields
        [Range(0f, 2f)]
        [SerializeField] private float m_intervalOfPlayerPositionChecking;
        [Range(0f, 50f)]
        [SerializeField] private float m_playerRadius;
        #endregion

        #region Fields
        private bool m_canMove;
        private NavMeshAgent m_navigationMeshAgent;

        public event Action OnWalkingStart, OnWalkingEnd;
        #endregion

        #region NPCBrain
        private void ObserveNPCDeath() 
        {
            NPC npc = GetComponent<NPC>();

            npc.OnDie += () =>
            {
                m_canMove = false;

                m_navigationMeshAgent.enabled = false;
            };;
        }

        private void ObservePlayer() 
        {
            Player player = FindObjectOfType<Player>();

            player.OnStun += () => m_canMove = false;
        }

        private IEnumerator GoToPlayer()
        {
            Transform playerTransform = FindObjectOfType<Player>().transform;

            ObservePlayer();

            Transform selfTransform = GetComponent<Transform>();

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();

            while (m_canMove)
            {
                Vector3 distanceToPlayer = (playerTransform.position - selfTransform.position);

                Vector3 nextPoint = selfTransform.position + distanceToPlayer.normalized * Mathf.Clamp(distanceToPlayer.magnitude - m_playerRadius, 0, 100000);

                navMeshAgent.SetDestination(nextPoint);

                yield return new WaitForSeconds(m_intervalOfPlayerPositionChecking);
            }

            m_navigationMeshAgent.enabled = false;

            OnWalkingEnd?.Invoke();
        }

        public void StopMovingToPlayer() 
        {
            m_canMove = false;
        }

        public void StartMovingToPlayer()
        {
            m_canMove = true;

            StartCoroutine(GoToPlayer());

            m_navigationMeshAgent = GetComponent<NavMeshAgent>();

            ObserveNPCDeath();

            OnWalkingStart?.Invoke();
        }

        #endregion
    }
}