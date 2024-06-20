using UnityEngine;

using IDragHandler = UnityEngine.EventSystems.IDragHandler;
using IEndDragHandler = UnityEngine.EventSystems.IEndDragHandler;

using PointerEventData = UnityEngine.EventSystems.PointerEventData;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class FixedJoystick : ControllableJoystick, IDragHandler, IEndDragHandler
    {
        #region SerializeFields
        [SerializeField] private RectTransform m_centerTransform;
        [SerializeField] private float m_radius;
        #endregion

        #region Fields
        private Vector2 m_deviation;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_radius < 0) 
            {
                m_radius = 0;
            }
        }

        private void OnEnable()
        {
            m_deviation = Vector2.zero;
        }
        #endregion

        #region IDragHandler
        public void OnDrag(PointerEventData eventData)
        {
            TryChangeDeviation(eventData);
        }
        #endregion

        #region IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            Vanish();

            m_deviation = Vector2.zero;
        }
        #endregion

        #region Joystick
        public override Vector2 GetDeviation() => m_deviation;

        public override Vector3 GetCenter() => m_centerTransform.position;
        #endregion

        #region FixedJoystick
        private void TryChangeDeviation(PointerEventData pointerEventData) 
        {
            if (m_isStickEnabled)
            {
                ChangeDeviation(pointerEventData);
            }
        }

        private void ChangeDeviation(PointerEventData eventData) 
        {
            Vector2 convertedFingerPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_centerTransform, eventData.position, Camera.main, out convertedFingerPosition);

            Vector2 unclampedDeviation = convertedFingerPosition;

            m_deviation = unclampedDeviation.normalized * Mathf.Clamp(unclampedDeviation.magnitude, 0, m_radius);
        }
        #endregion
    }
}