using UnityEngine;

using Image = UnityEngine.UI.Image;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class ControllableJoystickPresenter : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private ControllableJoystick m_joystick;
        [SerializeField] private RectTransform m_joystickRootTransform;
        [SerializeField] private RectTransform m_stickTransform;
        [Range(0f, 1f)]
        [SerializeField] private float m_joystickAlpha;
        #endregion

        #region Fields
        private Image[] m_joystickImages;
        #endregion

        #region MonoBehaviour
        private void Update()
        {
            TryMoveStick();

            m_joystickImages = m_joystickRootTransform.GetComponentsInChildren<Image>();

            m_joystick.OnSpawn += ShowJoystick;

            m_joystick.OnVanish += VanishJoystick;
        }
        #endregion

        #region JoystickPresenter
        private void TryMoveStick() 
        {
            m_stickTransform.localPosition = m_joystick.GetDeviation();
        }

        private void ChangeImageAlpha(float newAlphaOfImage, Image image) 
        {
            Color newJoystickColor = image.color;

            newJoystickColor.a = newAlphaOfImage;

            image.color = newJoystickColor;
        }

        private void VanishJoystick() 
        {
            foreach (var joystickSubImage in m_joystickImages) 
            {
                ChangeImageAlpha(0, joystickSubImage);
            }
        }

        private void ShowJoystick()
        {
            foreach (var joystickSubImage in m_joystickImages)
            {
                ChangeImageAlpha(m_joystickAlpha, joystickSubImage);
            }
        }
        #endregion
    }
}