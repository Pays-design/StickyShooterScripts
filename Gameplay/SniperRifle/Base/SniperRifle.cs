using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.Gameplay
{
    public class SniperRifle : Gun
    {
        #region SerializeFields
        [SerializeField] private GameObject m_bullet;
        [Range(0f, 3f)]
        [SerializeField] private float m_intervalOfShooting;
        [SerializeField] private LayerMask m_layersOfShooting;
        #endregion

        #region Fields
        private Coroutine m_coroutineOfShooting;
        #endregion

        #region Properties
        public Transform DirectionOfShootingTransform => m_directionOfShootingTransform;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_accelerationOfShooting < 0)
            {
                m_accelerationOfShooting = 0;
            }
        }
        #endregion

        #region Shotgun
        private IEnumerator PerformShooting()
        {
            while (true)
            {
                PerformShoot();

                yield return new WaitForSeconds(m_intervalOfShooting);
            }
        }

        public override void PerformShoot()
        {
            if (!Physics.Raycast(m_directionOfShootingTransform.position, m_directionOfShootingTransform.forward, out RaycastHit raycastHitInformation, 100000, m_layersOfShooting.value))
            {
                return;
            }

            GameObject bulletGameObject = Instantiate(m_bullet, m_directionOfShootingTransform.position, m_directionOfShootingTransform.rotation, null);

            Rigidbody bulletRigidbody = bulletGameObject.GetComponentInChildren<Rigidbody>();

            bulletRigidbody.AddForce(bulletGameObject.transform.forward * m_accelerationOfShooting, ForceMode.Acceleration);

            base.PerformShoot();
        }

        public void StartShooting()
        {
            m_coroutineOfShooting = StartCoroutine(PerformShooting());
        }

        public void EndShooting()
        {
            if (m_coroutineOfShooting != null)
            {
                StopCoroutine(m_coroutineOfShooting);
            }
        }
        #endregion
    }
}