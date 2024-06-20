using UnityEngine;
using System.Collections.Generic;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class ExplosiveTNT : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private float m_explosionRadius;
        [SerializeField] private ParticleSystem m_particlesToMake;
        [SerializeField] private LayerMask m_npcsLayerMask;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_explosionRadius < 0)
            {
                m_explosionRadius = 0;
            }
        }

        private void OnEnable()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.75F);

            Gizmos.DrawSphere(transform.position, m_explosionRadius);
        }

        #endregion

        private Creature[] FindCreaturesToKill() 
        {
            List<Creature> creatures = new List<Creature>();

            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, m_explosionRadius, m_npcsLayerMask, QueryTriggerInteraction.Collide);

            foreach (var collider in overlappedColliders)
            {
                if (collider.gameObject.HasComponent<Creature>())
                {
                    creatures.Add(collider.GetComponent<Creature>());
                }
            }

            return creatures.ToArray();
        }

        public void Explode()
        {
            Creature[] creaturesToKill = FindCreaturesToKill();

            Instantiate(m_particlesToMake, transform.position, Quaternion.identity).Play();

            foreach (var npcToKill in creaturesToKill)
            {
                npcToKill.Die();
            }

            Destroy(gameObject);
        }
    }
}