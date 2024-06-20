using UnityEngine;

using DrawShooter.Core.Gameplay;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.ArtificialIntelligence
{
    public class Sniper : MonoBehaviour
    {
        #region Fields
        private Transform m_transform, m_playerTransform;
        private SniperRifle m_rifle;
        private Coroutine m_shootingAtPlayerCoroutine;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_playerTransform = FindObjectOfType<Player>().transform;

            m_playerTransform.GetComponent<Player>().OnStun += StopShootingAtPlayer;
            m_playerTransform.GetComponent<Player>().OnDie += StopShootingAtPlayer;

            m_transform = GetComponent<Transform>();

            m_rifle = GetComponentInChildren<SniperRifle>();
        }

        private void FixedUpdate()
        {
            Vector3 directionToPlayer = m_playerTransform.position - m_transform.position;

            m_transform.forward = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);
        }
        #endregion

        #region Sniper
        private IEnumerator TryShootAtPlayer() 
        {
            while (true) 
            {
                yield return new WaitForSeconds(1f);

                Ray ray = new Ray(m_rifle.DirectionOfShootingTransform.position, m_rifle.DirectionOfShootingTransform.forward);

                if (Physics.Raycast(ray, out RaycastHit raycastHitInformation)) 
                {
                    if (raycastHitInformation.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        m_rifle.StartShooting();
                    }
                    else 
                    {
                        m_rifle.EndShooting();
                    }
                }
            }
        }

        public void StopShootingAtPlayer() 
        {
            Destroy(m_rifle);

            m_rifle.EndShooting();

            enabled = false;

            StopCoroutine(m_shootingAtPlayerCoroutine);

            m_playerTransform.GetComponent<Player>().OnStun -= StopShootingAtPlayer;
            m_playerTransform.GetComponent<Player>().OnDie -= StopShootingAtPlayer;
        }

        public void StartShootingAtPlayer() 
        {
            m_shootingAtPlayerCoroutine = StartCoroutine(TryShootAtPlayer());

            m_rifle.StartShooting();

            GetComponent<CreatureWithRagdoll>().OnDie += StopShootingAtPlayer;
        }
        #endregion
    }
}