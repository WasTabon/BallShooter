using BallShooter.Scripts.InputSystem;
using UnityEngine;
using PlayerSettings = BallShooter.Settings.Scriptables.PlayerSettings;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class PlayerController
    {
        private const string SettingsName = "PlayerSettings";
        private const float DecreaseEverySecond = 0.01f;
        
        private readonly InputManager _inputManager;
        private readonly GameObject _player;
        private readonly GameObject _bullet;
        private readonly Transform _bulletSpawnPos;

        private float _decreaseAmount;
        private float _timeLastDecrease;
        
        private Vector3 _bulletSize;
        private bool _isHold;
        
        public PlayerController(InputManager inputManager, GameObject player, GameObject bullet, Transform bulletSpawnPos)
        {
            _inputManager = inputManager;
            _player = player;
            _bullet = bullet;
            _bulletSpawnPos = bulletSpawnPos;
        }

        public void Initialize()
        {
            _bulletSize = Vector3.zero;
            _bullet.transform.localScale = _bulletSize;
            
            GetSettings();
            
            _inputManager.OnHoldPerformed += PrepareShoot;
        }

        private void PrepareShoot(bool state)
        {
            if (state)
            {
                _isHold = true;
                _timeLastDecrease += Time.deltaTime;
                if (_timeLastDecrease >= DecreaseEverySecond)
                {
                    DecreasePlayerSize();
                    _timeLastDecrease = 0f;
                }
            }
            else if (_isHold)
            {
                _isHold = false;
                Shoot();
            }
        }

        private void Shoot()
        {
            _bullet.transform.position = _bulletSpawnPos.position;
            _bullet.transform.localScale = _bulletSize;
            _bullet.SetActive(true);
        }
        
        private void DecreasePlayerSize()
        {
            Vector3 decreaseVector = new Vector3(_decreaseAmount, _decreaseAmount, _decreaseAmount);
            _player.transform.localScale -= decreaseVector;
            _bulletSize += decreaseVector;
        }
        
        private void GetSettings()
        {
            PlayerSettings playerSettings = Resources.Load<PlayerSettings>(SettingsName);
            _decreaseAmount = playerSettings.DecreaseAmount;
        }
    }
}
