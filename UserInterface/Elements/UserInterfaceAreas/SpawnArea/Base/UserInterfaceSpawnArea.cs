using UnityEngine;

using IDragHandler = UnityEngine.EventSystems.IDragHandler;
using IBeginDragHandler = UnityEngine.EventSystems.IBeginDragHandler;
using PointerEventData = UnityEngine.EventSystems.PointerEventData;

namespace DrawShooter.Core.UserInterface.Elements
{
    public abstract class UserInterfaceSpawnArea : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        #region UserInterfaceSpawnArea
        protected abstract void Spawn(PointerEventData pointerEventData);
        #endregion

        #region IBeginDragHandler
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            Spawn(pointerEventData);
        }
        #endregion

        #region IDragHandler
        public void OnDrag(PointerEventData eventData)
        {
        }
        #endregion
    }
}