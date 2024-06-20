using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class Bone : MonoBehaviour
    {
        #region Fields
        private Rigidbody m_rigidbody;
        private LayerMask m_wallsLayerMask;
        private bool m_canBeAttachedToWall;
        #endregion

        #region Properties
        public Rigidbody Rigidbody => m_rigidbody;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryBeAttachedToWall(collision.collider);
        }
        #endregion

        private void TryBeAttachedToWall(Collider enteredCollider)
        {
            if (m_canBeAttachedToWall && m_wallsLayerMask.ContainLayer(enteredCollider.gameObject.layer))
            {
                m_rigidbody.useGravity = false;
            }
        }

        public void BeBroken()
        {
            if (m_rigidbody == null)
                return;

            m_rigidbody.drag = 50;

            m_rigidbody.isKinematic = false;
        }

        public void BeWhole(LayerMask wallsLayerMask)
        {
            m_rigidbody.isKinematic = true;

            m_wallsLayerMask = wallsLayerMask;

            m_canBeAttachedToWall = true;
        }
    }
}