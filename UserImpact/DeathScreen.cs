using UnityEngine;
using UnityEngine.UI;

namespace DrawShooter.Core.UserImpact
{
    [RequireComponent(typeof(Image))]
    public class DeathScreen : MonoBehaviour
    {
        #region SerializeFields
        [Range(0f, 3f)]
        [SerializeField] private float m_timeOfFade;
        #endregion

        #region MonoBehaviour
        private void OnEnable()
        {
            Image image = GetComponent<Image>();

            image.CrossFadeAlpha(0, m_timeOfFade, true);
        }
        #endregion
    }
}