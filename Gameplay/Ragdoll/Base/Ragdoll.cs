using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace DrawShooter.Core.Gameplay
{
    public class Ragdoll : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private LayerMask m_wallsLayerMask;
        #endregion

        #region Fields
        private Bone[] m_bones;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            FindBones();

            BeAlive();
        }
        #endregion

        #region Ragdoll
        private IEnumerator BeDeadInNextFrame() 
        {
            yield return new WaitForEndOfFrame();

            foreach (var bone in m_bones)
            {
                if(bone.gameObject.layer == LayerMask.NameToLayer("EnemyHand"))
                {
                    bone.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                }

                bone.BeBroken();
            }
        }

        private void FindBones()
        {
            Rigidbody[] bonesRigidBodies = GetComponentsInChildren<Rigidbody>();

            int countOfBones = bonesRigidBodies.Length;

            m_bones = new Bone[countOfBones];

            for (int indexOfBone = 0; indexOfBone < countOfBones; indexOfBone++)
            {
                m_bones[indexOfBone] = bonesRigidBodies[indexOfBone].gameObject.AddComponent<Bone>();
            }
        }

        public void BeDead()
        {
            StartCoroutine(BeDeadInNextFrame());
        }

        public void BeAlive()
        {
            foreach (var bone in m_bones)
            {
                bone.BeWhole(m_wallsLayerMask);
            }
        }

        #endregion
    }
}