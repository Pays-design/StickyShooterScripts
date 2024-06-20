using UnityEngine;

using Button = UnityEngine.UI.Button;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DrawShooter.Core.UserInterface.Elements
{
    [RequireComponent(typeof(Button))]
    public class ReplayButton : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Replay);
        }
        #endregion

        #region ReplayButton
        private void Replay() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        #endregion
    }
}