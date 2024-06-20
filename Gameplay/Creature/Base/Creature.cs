using UnityEngine;

using Action = System.Action;

namespace DrawShooter.Core.Gameplay
{
    public abstract class Creature : MonoBehaviour
    {
        private bool m_isDead;

        public event Action OnDie;

        public bool IsDead => m_isDead;

        public virtual void Die() 
        {
            if (!m_isDead)
            {
                OnDie?.Invoke();

                m_isDead = true;
            }
        }
    }
}