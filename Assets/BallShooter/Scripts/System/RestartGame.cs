using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BallShooter.Scripts.System
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private Button[] _endGameButtons;

        private void Start()
        {
            foreach (Button button in _endGameButtons)
            {
                button.onClick.AddListener(Restart);
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
