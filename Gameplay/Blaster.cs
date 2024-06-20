using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.Gameplay
{
    public class Blaster : Gun
    {
        #region SerializeFields
        [SerializeField] private GameObject m_bullet;
        [SerializeField] private Transform m_playerTransform;
        [Range(1f, 5f)]
        [SerializeField] private float m_bulletScaleFactor;
        [SerializeField] private float m_bulletScalePeriod;
        #endregion

        #region Fields
        private Vector3 m_firstPointOfBullet, m_secondPointOfBullet;
        private GameObject m_spawnedBullet;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            ValidateAccelerationOfShooting();
        }
        #endregion

        #region Shotgun
        private void ValidateBulletScalePeriod() 
        {
            m_bulletScalePeriod = m_bulletScalePeriod > 0 ? m_bulletScalePeriod : 0;
        }

        private void ValidateAccelerationOfShooting()
        {
            if (m_accelerationOfShooting < 0)
            {
                m_accelerationOfShooting = 0;
            }
        }

        private IEnumerator ScaleBullet(Transform bulletTransform) 
        {
            float bulletScaleTime = 0;

            Vector3 startBulletLocalScale = bulletTransform.localScale;
             
            while(bulletScaleTime < m_bulletScalePeriod)
            {
                bulletScaleTime = Mathf.Clamp(bulletScaleTime + Time.fixedDeltaTime, 0, m_bulletScalePeriod);

                bulletTransform.localScale = Vector3.Lerp(startBulletLocalScale, startBulletLocalScale * m_bulletScaleFactor, bulletScaleTime / m_bulletScalePeriod);

                yield return new WaitForFixedUpdate();
            }
        }

        public override void PerformShoot()
        {
            foreach(var bulletRigidbody in m_spawnedBullet.GetComponentsInChildren<Rigidbody>())
            {
                bulletRigidbody.isKinematic = false;

                bulletRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                bulletRigidbody.AddForce((m_spawnedBullet.transform.position - m_playerTransform.position).normalized * m_accelerationOfShooting, ForceMode.Acceleration);
            }

            m_spawnedBullet.transform.parent = null;

            m_spawnedBullet.GetComponentInChildren<SlimeBullet>().enabled = true;

            StartCoroutine(ScaleBullet(m_spawnedBullet.transform));

            m_spawnedBullet = null;

            base.PerformShoot();
        }

        public void SetupBulletPoints(Vector3 firstPoint, Vector3 secondPoint) 
        {
            (m_firstPointOfBullet, m_secondPointOfBullet) = (firstPoint, secondPoint);

            m_spawnedBullet.transform.right = (m_secondPointOfBullet - m_firstPointOfBullet).normalized;

            m_spawnedBullet.transform.position = (firstPoint + secondPoint) / 2;

            Vector3 newBulletScale = m_spawnedBullet.transform.localScale;

            newBulletScale.x = (m_firstPointOfBullet - m_secondPointOfBullet).magnitude;

            m_spawnedBullet.transform.localScale = newBulletScale;
        }

        public void CreateBullet(Vector3 firstPoint, Vector3 secondPoint) 
        {
            m_spawnedBullet = Instantiate(m_bullet, Vector3.zero, Quaternion.identity, m_directionOfShootingTransform);

            m_spawnedBullet.GetComponentInChildren<SlimeBullet>().enabled = false;

            SetupBulletPoints(firstPoint, secondPoint);
        }
        #endregion
    }
}