using UnityEngine;

using Action = System.Action;

namespace DrawShooter.Core.Gameplay
{
    public abstract class Gun : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] protected float m_accelerationOfShooting;
        [SerializeField] protected Transform m_directionOfShootingTransform;
        #endregion

        #region Fields
        public event Action OnShot;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if(m_accelerationOfShooting < 0)
            {
                m_accelerationOfShooting = 0;
            }
        }
        #endregion

        #region Gun
        public virtual void PerformShoot()
        {
            OnShot?.Invoke();
        }
        #endregion
    }
}