using UnityEngine;

using DrawShooter.Core.Gameplay;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.UserImpact
{
    public class HandShaker : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Transform m_axisOfRotationTransform;
        [SerializeField] private float m_shakingAmplitudeInAngles, m_shakingFrequence;
        [SerializeField] private Transform m_handTransform;
        #endregion

        #region Fields
        private Quaternion m_startHandRotation;
        private bool m_canShakeHand;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            AutomaticPlayerMover.GetInstance().OnPlayerMovingStart += StartHandShaking;

            AutomaticPlayerMover.GetInstance().OnPlayerMovingEnd += (_) => EndHandShaking();

            m_startHandRotation = m_handTransform.localRotation;
        }

        private void OnValidate()
        {
            ValidateShakingAmplitude();

            ValidateShakingFrequence();
        }
        #endregion

        #region HandShaker
        private void ValidateShakingAmplitude()
        {
            m_shakingAmplitudeInAngles = m_shakingAmplitudeInAngles < 0 ? 0 : m_shakingAmplitudeInAngles;
        }

        private void ValidateShakingFrequence() 
        {
            m_shakingFrequence = m_shakingFrequence < 0 ? 0 : m_shakingFrequence;
        }

        private IEnumerator RotateHandToQuaternion(Quaternion endRotation) 
        {
            while (Quaternion.Angle(m_handTransform.localRotation, endRotation) > 0)
            {
                m_handTransform.localRotation = Quaternion.RotateTowards(m_handTransform.localRotation, endRotation, m_shakingFrequence * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator ShakeHandUp() 
        {
            Quaternion finalRotation = m_startHandRotation * Quaternion.AngleAxis(-m_shakingAmplitudeInAngles, m_axisOfRotationTransform.InverseTransformDirection(m_axisOfRotationTransform.forward));

            yield return RotateHandToQuaternion(finalRotation);
        }

        private IEnumerator ShakeHandDown() 
        {
            Quaternion finalRotation = m_startHandRotation * Quaternion.AngleAxis(m_shakingAmplitudeInAngles, m_axisOfRotationTransform.InverseTransformDirection(m_axisOfRotationTransform.forward));

            yield return RotateHandToQuaternion(finalRotation);
        }

        private IEnumerator MakeHandCalm() 
        {
            yield return RotateHandToQuaternion(m_startHandRotation);
        }

        private IEnumerator ShakeHand()
        {
            var automaticPlayerMover = AutomaticPlayerMover.GetInstance();

            while (m_canShakeHand)
            {
                if (!automaticPlayerMover.IsPlayerJumping)
                {
                    yield return ShakeHandUp();

                    yield return ShakeHandDown();
                }

                yield return new WaitForEndOfFrame();
            }

            yield return MakeHandCalm();
        }

        private void EndHandShaking() 
        {
            m_canShakeHand = false;
        }

        private void StartHandShaking(float distance)
        {
            if (distance == 0)
            {
                return;
            }

            m_canShakeHand = true;

            StartCoroutine(ShakeHand());
        }
        #endregion
    }
}