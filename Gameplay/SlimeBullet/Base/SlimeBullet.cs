using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class SlimeBullet : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_track;
        [SerializeField] private Transform m_centerTransform;
        [SerializeField] private float m_enemyHeight;
        #endregion

        #region Fields
        private Rigidbody[] m_parts;
        private Transform m_transformOfSlime;
        private bool m_isOnTheWall;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if(m_enemyHeight < 0)
            {
                m_enemyHeight = 0;
            }
        }

        private void Start()
        {
            GetParts();

            m_transformOfSlime = new GameObject().transform;
        }

        private void FixedUpdate()
        {
            m_transformOfSlime.position = m_centerTransform.position;
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            TryKillPossibleCreature(enteredCollider);

            TryDemolishTNT(enteredCollider);
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryBeAttachedToWall(collision);
        }
        #endregion

        #region Bullet
        private void SpawnTrack(Collision collision)
        {
            GameObject instantiatedTrack = Instantiate(m_track, collision.contacts[0].point, Quaternion.identity, null);

            instantiatedTrack.transform.forward = collision.contacts[0].normal;
        }

        private void GetParts() 
        {
            m_parts = transform.parent.GetComponentsInChildren<Rigidbody>();
        }

        private void TryBeAttachedToWall(Collision collision) 
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                GetComponent<Rigidbody>().isKinematic = true;

                SpawnTrack(collision);

                m_isOnTheWall = true;
            }
        }

        private void TryKillPossibleCreature(Collider possibleCreatureCollider) 
        {
            if (possibleCreatureCollider.gameObject.HasComponent<Creature>() && !(possibleCreatureCollider.GetComponent<Creature>() is Player) && !m_isOnTheWall) 
            {
                if (!possibleCreatureCollider.gameObject.GetComponent<Creature>().IsDead)
                {
                    possibleCreatureCollider.gameObject.GetComponent<Creature>().Die();

                    possibleCreatureCollider.transform.parent = m_transformOfSlime;

                    possibleCreatureCollider.transform.localPosition = new Vector3(0, possibleCreatureCollider.transform.localPosition.y, 0);
                }
            }
        }

        private void TryDemolishTNT(Collider possibleTNTCollider) 
        {
            if (possibleTNTCollider.gameObject.HasComponent<ExplosiveTNT>()) 
            {
                possibleTNTCollider.GetComponent<ExplosiveTNT>().Explode();

                Destroy(gameObject);
            }
        }
        #endregion
    }
}