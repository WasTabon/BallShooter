using BallShooter.Scripts.InputSystem;
using UnityEngine;
using PlayerSettings = BallShooter.Settings.Scriptables.PlayerSettings;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class PlayerController
    {
        private const string SettingsName = "PlayerSettings";
        
        private readonly InputManager _inputManager;
        private readonly GameObject _player;
        private readonly GameObject _bullet;

        private float _decreaseAmount;
        
        private Vector3 _bulletSize;
        private bool _isHold;
        
        public PlayerController(InputManager inputManager, GameObject player, GameObject bullet)
        {
            _inputManager = inputManager;
            _player = player;
            _bullet = bullet;
        }

        public void Initialize()
        {
            PlayerSettings playerSettings = Resources.Load<PlayerSettings>(SettingsName);
            _decreaseAmount = playerSettings.DecreaseAmount;
            
            _inputManager.OnHoldPerformed += Shoot;
        }

        private void Shoot(bool state)
        {
            
            
            if (state)
            {
                _isHold = true;
            }
            else if (_isHold)
            {
                _isHold = false;
            }
        }
    }
}
