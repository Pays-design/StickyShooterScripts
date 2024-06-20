using GameObject = UnityEngine.GameObject;
using Component = UnityEngine.Component;

namespace DrawShooter.Core.UnityExtensions
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<TypeOfComponent>(this GameObject gameObject) where TypeOfComponent : Component
        {
            return gameObject.TryGetComponent(out TypeOfComponent component);
        }
    }
}