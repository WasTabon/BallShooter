using UnityEngine;

namespace BallShooter.Scripts.Ui
{
    public class UIController
    {
        private readonly UIView _uiView;

        public UIController(UIView uiView)
        {
            _uiView = uiView;
        }

        public void FinishGame()
        {
            _uiView.PanelFinish.SetActive(true);
        }

        public void PlayerDied()
        {
            _uiView.PanelDied.SetActive(true);
        }
    }
}
