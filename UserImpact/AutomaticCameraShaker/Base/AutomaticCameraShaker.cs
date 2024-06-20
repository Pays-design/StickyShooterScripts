using UnityEngine;

using DrawShooter.Core.Gameplay;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.UserImpact
{
    public class AutomaticCameraShaker : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private CameraShaker m_cameraShaker;
        #endregion

        #region Fields
        private bool m_canShakeCamera;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            AutomaticPlayerMover.GetInstance().OnPlayerMovingStart += TryStartCameraShake;

            AutomaticPlayerMover.GetInstance().OnPlayerMovingEnd += (_) => EndCameraShake();
        }
        #endregion

        #region AutomaticCameraShaker
        private IEnumerator ShakeCamera() 
        {
            var automaticPlayerMover = AutomaticPlayerMover.GetInstance();

            while(m_canShakeCamera)
            {
                if (!automaticPlayerMover.IsPlayerJumping)
                {
                    m_cameraShaker.shakeOnce = true;
                    m_cameraShaker.Shake();

                    while (m_cameraShaker.isShaking)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void TryStartCameraShake(float distance) 
        {
            if(distance > 0)
            {
                m_canShakeCamera = true;

                StartCoroutine(ShakeCamera());
            }
        }

        private void EndCameraShake() 
        {
            m_canShakeCamera = false;
        }
        #endregion
    }
}