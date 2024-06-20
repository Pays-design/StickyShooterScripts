using UnityEngine;

using Debug = UnityEngine.Debug;

namespace DrawShooter.Core.UnityExtensions
{
    public abstract class ClassicNotCreatableMonoBehaviourSingletone<SingletoneType> : MonoBehaviour where SingletoneType : ClassicNotCreatableMonoBehaviourSingletone<SingletoneType>
    {
        #region ClassicNotCreatableMonoBehaviourSingletone
        private static SingletoneType s_instance;

        private static void TryReportErrorAboutInstancesCount(SingletoneType[] instances) 
        {
            if (instances.Length > 1)
            {
                Debug.LogError("There are many: " + typeof(SingletoneType).Name);
            }
            else if (instances.Length == 0)
            {
                Debug.LogError("There aren't: " + typeof(SingletoneType).Name);
            }
        }

        private static void FindInstance() 
        {
            var instances = FindObjectsOfType<SingletoneType>();

            TryReportErrorAboutInstancesCount(instances);

            s_instance = instances.Length == 1? instances[0] : null;
        }

        private static void TryFindInstance() 
        {
            if (s_instance == null)
            {
                FindInstance();
            }
        }

        public static SingletoneType GetInstance()
        {
            TryFindInstance();

            return s_instance;
        }
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            var instances = FindObjectsOfType<SingletoneType>();

            TryReportErrorAboutInstancesCount(instances);
        }
        #endregion
    }
}