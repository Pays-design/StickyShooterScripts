using System;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

using DrawShooter.Core.UnityExtensions;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.Gameplay
{
    [RequireComponent(typeof(NPCGroupDeathObserver))]
    public class AutomaticPlayerMover : ClassicNotCreatableMonoBehaviourSingletone<AutomaticPlayerMover>
    {
        #region SerializeFields
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private Spline m_levelPath;
        [SerializeField] private List<float> m_distancesToLevelParts;
        [SerializeField] private float m_speedOfPlayerMoving;
        [SerializeField] private float m_speedOfPlayerJumping;
        #endregion

        #region Fields
        private float m_playerPassedDistance;
        private IEnumerator<float> m_distancesToReachChain;
        private bool m_isPlayerJumping;

        public Action<NPCGroup> OnPlayerMovingEnd;
        public Action<float> OnPlayerMovingStart;
        #endregion

        #region Properties
        public bool IsPlayerJumping => m_isPlayerJumping;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            ValidateSpeedOfPlayerMoving();

            ValidateDistancesToLevelParts();

            ValidateSpeedOfPlayerJumping();
        }

        protected override void Awake()
        {
            m_distancesToReachChain = m_distancesToLevelParts.GetEnumerator();

            ObservePlayerMovingStart();
        }
        #endregion

        #region AutomaticPlayerMover
        private void ValidateSpeedOfPlayerJumping() 
        {
            if(m_speedOfPlayerJumping < m_speedOfPlayerMoving)
            {
                m_speedOfPlayerJumping = m_speedOfPlayerMoving;
            }
        }

        private void ValidateDistancesToLevelParts() 
        {
            for(int indexOfDistanceToLevelPart = 0; indexOfDistanceToLevelPart < m_distancesToLevelParts.Count; indexOfDistanceToLevelPart++)
            {
                float distanceToLevelPart = m_distancesToLevelParts[indexOfDistanceToLevelPart];

                m_distancesToLevelParts[indexOfDistanceToLevelPart] = distanceToLevelPart > 0 ? distanceToLevelPart : 0;
            }
        }

        private void ValidateSpeedOfPlayerMoving() 
        {
            if(m_speedOfPlayerMoving < 0)
            {
                m_speedOfPlayerMoving = 0;
            }
        }

        private IEnumerator MovePlayerWithoutInforming(float distanceToReach)
        {
            float nextDistance = Mathf.MoveTowards(m_playerPassedDistance, distanceToReach, (m_isPlayerJumping ? m_speedOfPlayerJumping : m_speedOfPlayerMoving) * Time.fixedDeltaTime);

            while (m_playerPassedDistance < distanceToReach && nextDistance < m_levelPath.Length)
            {
                m_playerPassedDistance = nextDistance;

                var sample = m_levelPath.GetSampleAtDistance(m_playerPassedDistance);

                m_playerTransform.position = sample.location;
                m_playerTransform.rotation = m_isPlayerJumping ? m_playerTransform.rotation : sample.Rotation;

                yield return new WaitForFixedUpdate();

                nextDistance = Mathf.MoveTowards(m_playerPassedDistance, distanceToReach, (m_isPlayerJumping ? m_speedOfPlayerJumping : m_speedOfPlayerMoving) * Time.fixedDeltaTime);
            }
        }

        private IEnumerator MovePlayer(float distanceToReach, NPCGroup npcGroup)
        {
            OnPlayerMovingStart?.Invoke(distanceToReach);

            yield return MovePlayerWithoutInforming(distanceToReach);

            OnPlayerMovingEnd?.Invoke(npcGroup);
        }

        private void StartPlayerMovement(NPCGroup npcGroup) 
        {
            if (m_distancesToReachChain.MoveNext())
            {
                StartCoroutine(MovePlayer(m_distancesToReachChain.Current, npcGroup));
            }
        }

        private void ObservePlayerMovingStart() 
        {
            var npcGroupDeathObserver = GetComponent<NPCGroupDeathObserver>();

            npcGroupDeathObserver.OnGroupDeath += StartPlayerMovement;
            npcGroupDeathObserver.OnNetworkDeath += () => StartPlayerMovement(null);
        }

        public void ChangePlayerJumpBehaviour() 
        {
            m_isPlayerJumping = !m_isPlayerJumping;
        }
        #endregion
    }
}