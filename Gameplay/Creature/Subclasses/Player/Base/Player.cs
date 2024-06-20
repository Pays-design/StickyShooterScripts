using UnityEngine;
using Action = System.Action;

namespace DrawShooter.Core.Gameplay
{
    public class Player : Creature
    {
        #region Fields
        public event Action OnStun;
        #endregion

        #region Creature
        public override void Die()
        {
            base.Die();

            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void Stun() 
        {
            OnStun?.Invoke();
        }
        #endregion
    }
}