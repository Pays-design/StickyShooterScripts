using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class JumpTrigger : MonoBehaviour
    {
        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<Player>()) 
            {
                Trigger();
            }
        }
        #endregion

        #region JumpTrigger
        private void Trigger() 
        {
            AutomaticPlayerMover.GetInstance().ChangePlayerJumpBehaviour();
        }
        #endregion
    }
}