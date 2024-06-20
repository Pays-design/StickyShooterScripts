
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

using MonoBehaviour = UnityEngine.MonoBehaviour;

namespace DrawShooter.Core.UserInterface.Elements
{
    public abstract class Joystick : MonoBehaviour
    {
        public abstract Vector3 GetCenter();

        public abstract Vector2 GetDeviation();
    }
}
