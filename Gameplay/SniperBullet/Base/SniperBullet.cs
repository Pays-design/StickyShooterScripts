using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class SniperBullet : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_track;
        #endregion

        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryKillPossibleCreature(collision.collider);

            TrySpawnTrack(collision.contacts[0]);

            Destroy(gameObject);
        }
        #endregion

        #region Bullet
        private void TrySpawnTrack(ContactPoint contactPoint)
        {
            if (contactPoint.otherCollider.gameObject.layer == LayerMask.NameToLayer("Enemy") || contactPoint.otherCollider.gameObject.layer == LayerMask.NameToLayer("Player") || contactPoint.otherCollider.isTrigger == true)
            {
                return;
            }

            GameObject instantiatedTrack = Instantiate(m_track, contactPoint.point, Quaternion.identity, null);

            instantiatedTrack.transform.forward = contactPoint.normal;
        }

        private void TryKillPossibleCreature(Collider possibleCreatureCollider)
        {
            if (possibleCreatureCollider.gameObject.HasComponent<Creature>())
            {
                possibleCreatureCollider.gameObject.GetComponent<Creature>().Die();
            }
        }

        private void TryDemolishTNT(Collider possibleTNTCollider)
        {
            if (possibleTNTCollider.gameObject.HasComponent<ExplosiveTNT>())
            {
                possibleTNTCollider.GetComponent<ExplosiveTNT>().Explode();
            }
        }

        public void TryKillSomethingWithRaycast(Vector3 startPosition, Vector3 directionOfShooting)
        {
            Ray ray = new Ray(startPosition, directionOfShooting);

            if (Physics.Raycast(ray, out RaycastHit raycastHitInformation))
            {
                TryDemolishTNT(raycastHitInformation.collider);
            }
        }
        #endregion
    }
}