using UnityEngine;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class PlayerMover : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private Joystick m_joystick;
        [SerializeField] private float m_speedOfPlayerMoving;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            InspectSpeed();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }
        #endregion

        #region PlayerMover
        private void InspectSpeed() 
        {
            if (m_speedOfPlayerMoving < 0) 
            {
                m_speedOfPlayerMoving = 0;
            }
        }

        private void MovePlayer() 
        {
            Vector2 joystickDeviation = m_joystick.GetDeviation();

            Vector3 untranslatedVectorOfPlayerMoving = new Vector3(joystickDeviation.x, 0, joystickDeviation.y).normalized * m_speedOfPlayerMoving * Time.fixedDeltaTime;

            Vector3 translatedVectorOfPlayerMoving = untranslatedVectorOfPlayerMoving.x * new Vector3(m_playerTransform.right.x, 0, m_playerTransform.right.z) + untranslatedVectorOfPlayerMoving.z * new Vector3(m_playerTransform.forward.x, 0, m_playerTransform.forward.z);

            m_playerTransform.position += translatedVectorOfPlayerMoving;
        }
        #endregion
    }
}