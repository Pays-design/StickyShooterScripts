using UnityEngine;

using DrawShooter.Core.Gameplay;

using PointerEventData = UnityEngine.EventSystems.PointerEventData;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class JoystickSpawnArea : UserInterfaceSpawnArea
    {
        #region SerializeFields
        [SerializeField] private RectTransform m_joystickToSpawnTransform;
        [SerializeField] private ControllableJoystick m_joystickToControl;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            AutomaticPlayerMover.GetInstance().OnPlayerMovingEnd += (_) => gameObject.SetActive(true);

            AutomaticPlayerMover.GetInstance().OnPlayerMovingStart += (_) => gameObject.SetActive(false);
        }
        #endregion

        #region UserInterfaceSpawnArea
        protected override void Spawn(PointerEventData pointerEventData)
        {
            m_joystickToControl.Enable();

            Vector2 newJoystickPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_joystickToSpawnTransform.parent, pointerEventData.position, Camera.main, out newJoystickPosition);

            m_joystickToSpawnTransform.localPosition = newJoystickPosition;

            pointerEventData.pointerDrag = m_joystickToControl.gameObject;
        }
        #endregion
    }
}