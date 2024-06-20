using UnityEngine;

using Button = UnityEngine.UI.Button;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DrawShooter.Core.UserInterface.Elements
{
    [RequireComponent(typeof(Button))]
    public class ToNextLevelButton : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(LoadNextLevel);
        }
        #endregion

        #region ReplayButton
        private void LoadNextLevel()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        #endregion
    }
}