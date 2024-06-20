using UnityEngine;

using DrawShooter.Core.UnityExtensions;

using IEnumerator = System.Collections.IEnumerator;
using Action = System.Action;

namespace DrawShooter.Core.Gameplay
{
    public class NPC : CreatureWithRagdoll
    {
        #region SerializeFields
        [Range(0f, 1f)]
        [SerializeField] private float m_speedOfRotation, m_minimumAngleOfRotationStopping;
        [Range(0f, 2f)]
        [SerializeField] private float m_periodOfWaitingBeforePlayerKill;
        #endregion

        #region Fields
        public event Action OnPlayerKill;
        #endregion

        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            TryKillPossiblePlayer(enteredCollider);
        }
        #endregion

        #region NPC
        private IEnumerator RotateToPlayerAndKillHim(Transform playerTransform, Player player) 
        {
            Transform selfTransform = GetComponent<Transform>();

            Vector3 distanceVectorToPlayer = playerTransform.position - selfTransform.position;

            distanceVectorToPlayer.y = 0;

            while (Vector3.Angle(selfTransform.forward, distanceVectorToPlayer) > m_minimumAngleOfRotationStopping) 
            {
                selfTransform.forward = Vector3.Lerp(selfTransform.forward, distanceVectorToPlayer, m_speedOfRotation);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(m_periodOfWaitingBeforePlayerKill);

            player.Die();
        }

        private void StartToPlayerRotationCoroutine(Transform playerTransform, Player player) 
        {
            StartCoroutine(RotateToPlayerAndKillHim(playerTransform, player));
        }

        private void TryKillPossiblePlayer(Collider possiblePlayerCollider) 
        {
            if (!possiblePlayerCollider.gameObject.HasComponent<Player>()) 
            {
                return;
            }

            DisableCollider();

            StartToPlayerRotationCoroutine(possiblePlayerCollider.transform, possiblePlayerCollider.GetComponent<Player>());

            possiblePlayerCollider.GetComponent<Player>().Stun();

            OnPlayerKill?.Invoke();
        }

        private void DisableCollider() 
        {
            Collider collider = GetComponent<Collider>();

            Destroy(collider);
        }
        #endregion

        #region Creature
        public override void Die()
        {
            base.Die();

            gameObject.layer = LayerMask.NameToLayer("Enemy");

            DisableCollider();

            Destroy(GetComponent<Rigidbody>());
        }
        #endregion
    }
}