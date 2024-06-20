using UnityEngine;

using DrawShooter.Core.UnityExtensions;

using Action = System.Action;

namespace DrawShooter.Core.Gameplay
{
    public class LevelFinish : MonoBehaviour
    {
        #region Fields
        public event Action OnPlayerEnter;
        #endregion

        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            TryIndicateAboutPlayerEnter(enteredCollider);
        }
        #endregion

        #region LevelFinish
        private void TryIndicateAboutPlayerEnter(Collider enteredCollider) 
        {
            if (enteredCollider.gameObject.HasComponent<Player>()) 
            {
                OnPlayerEnter?.Invoke();

                enteredCollider.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        #endregion
    }
}