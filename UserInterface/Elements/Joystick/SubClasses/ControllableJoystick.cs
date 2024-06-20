using Action = System.Action;

namespace DrawShooter.Core.UserInterface.Elements
{
    public abstract class ControllableJoystick : Joystick
    {
        #region Fields
        protected bool m_isStickEnabled;

        public event Action OnSpawn, OnVanish;
        #endregion

        #region ObservableJoystick
        public virtual void Enable() 
        {
            m_isStickEnabled = true;

            OnSpawn?.Invoke();
        }

        public virtual void Vanish() 
        {
            m_isStickEnabled = false;

            OnVanish?.Invoke();
        }
        #endregion
    }
}