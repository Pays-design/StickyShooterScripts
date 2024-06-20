using UnityEngine;
using UnityEngine.EventSystems;

using Blaster = DrawShooter.Core.Gameplay.Blaster;

namespace DrawShooter.Core.UserInterface.Elements
{
    public class BulletCreationArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region SerializeFields
        [SerializeField] private Blaster m_blaster;
        #endregion

        #region Fields
        private Camera m_mainCamera;
        private Vector3 m_firstPointOfBullet;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_mainCamera = Camera.main;
        }
        #endregion

        #region IBeginDragHandler
        public void OnBeginDrag(PointerEventData eventData)
        {
            m_firstPointOfBullet = m_mainCamera.ScreenPointToRay(eventData.position).origin + m_mainCamera.ScreenPointToRay(eventData.position).direction * 2;

            m_blaster.CreateBullet(m_firstPointOfBullet, m_firstPointOfBullet);
        }
        #endregion

        #region IDragHandler
        public void OnDrag(PointerEventData eventData)
        {
            m_blaster.SetupBulletPoints(m_firstPointOfBullet, m_mainCamera.ScreenPointToRay(eventData.position).origin + m_mainCamera.ScreenPointToRay(eventData.position).direction * 2);
        }
        #endregion

        #region IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            m_blaster.PerformShoot();
        }
        #endregion
    }
}