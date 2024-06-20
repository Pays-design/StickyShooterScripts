using UnityEngine;

namespace DrawShooter.Core.UnityExtensions
{
    public static class LayerMaskExtension
    {
        public static bool ContainLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}