using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class DeathZone : MonoBehaviour
    {
        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            TryKillPlayerIfItPlayer(enteredCollider);
        }
        #endregion

        #region DeathZone
        private void TryKillPlayerIfItPlayer(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<Player>())
            {
                enteredCollider.GetComponent<Player>().Die();

                enteredCollider.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        #endregion
    }
}
