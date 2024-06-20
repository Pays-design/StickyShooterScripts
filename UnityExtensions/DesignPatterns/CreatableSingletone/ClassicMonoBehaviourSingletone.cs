using GameObject = UnityEngine.GameObject;
using MonoBehaviour = UnityEngine.MonoBehaviour;

namespace DrawShooter.Core.UnityExtensions
{
    public abstract class ClassicMonoBehaviourSingletone<SingletoneType> : MonoBehaviour where SingletoneType : ClassicMonoBehaviourSingletone<SingletoneType>
    {
        #region Singletone
        private static SingletoneType s_instance;

        private static void CreateAndSetNewInstance()
        {
            GameObject instanceGameObject = new GameObject("SingletoneInstance: " + typeof(SingletoneType).Name);

            s_instance = instanceGameObject.AddComponent<SingletoneType>();
        }

        public static SingletoneType GetInstance()
        {
            if (s_instance == null)
            {
                CreateAndSetNewInstance();
            }

            return s_instance;
        }
        #endregion
    }
}
