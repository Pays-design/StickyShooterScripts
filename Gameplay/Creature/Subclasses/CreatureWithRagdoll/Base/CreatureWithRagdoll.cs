using UnityEngine;

namespace DrawShooter.Core.Gameplay
{
    [RequireComponent(typeof(Ragdoll))]
    public class CreatureWithRagdoll : Creature
    {
        #region Fields
        private Ragdoll m_ragdoll;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_ragdoll = GetComponent<Ragdoll>();
        }
        #endregion

        #region Creature
        public override void Die()
        {
            base.Die();

            m_ragdoll.BeDead();
        }
        #endregion
    }
}