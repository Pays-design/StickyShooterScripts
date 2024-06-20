using YsoCorp.GameUtils;

using MonoBehaviour = UnityEngine.MonoBehaviour;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

using LevelFinish = DrawShooter.Core.Gameplay.LevelFinish;
using Player = DrawShooter.Core.Gameplay.Player;

namespace DrawShooter.Core.Analytics
{
    public class YCInitializer : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            InitializeYC();

            FindObjectOfType<LevelFinish>().OnPlayerEnter += MessageYCAboutLevelFinish;

            FindObjectOfType<Player>().OnDie += MessageYCAboutLevelFail;
        }
        #endregion

        #region 
        private void InitializeYC() 
        {
            YCManager.instance.OnGameStarted(SceneManager.GetActiveScene().buildIndex);
        }

        private void MessageYCAboutLevelFail() 
        {
            YCManager.instance.OnGameFinished(false);
        }

        private void MessageYCAboutLevelFinish() 
        {
            YCManager.instance.OnGameFinished(true);
        }
        #endregion
    }
}