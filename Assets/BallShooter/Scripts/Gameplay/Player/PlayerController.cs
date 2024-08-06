using BallShooter.Scripts.InputSystem;
using DG.Tweening;
using UnityEngine;
using PlayerSettings = BallShooter.Settings.Scriptables.PlayerSettings;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class PlayerController
    {
        private const string SettingsName = "PlayerSettings";
        private const float DecreaseEverySecond = 0.01f;
        
        public Bullet Bullet { get; private set; }
        
        private readonly InputManager _inputManager;
        private readonly GameObject _playerInScene;
        private readonly GameObject _bulletInScene;
        private readonly Transform _bulletSpawnPos;

        private float _decreaseAmount;
        private float _timeLastDecrease;
        
        private Vector3 _bulletSize;
        private bool _isHold;
        
        public PlayerController(InputManager inputManager, GameObject player, GameObject bullet, Transform bulletSpawnPos)
        {
            _inputManager = inputManager;
            _playerInScene = player;
            _bulletInScene = bullet;
            _bulletSpawnPos = bulletSpawnPos;
            Bullet = new Bullet();
        }

        public void Initialize()
        {
            _bulletSize = Vector3.zero;
            _bulletInScene.transform.localScale = _bulletSize;
            
            GetSettings();
            
            _inputManager.OnHoldPerformed += PrepareShoot;
        }

        private void PrepareShoot(bool state)
        {
            if (state)
            {
                if (_isHold = false)
                {
                    _bulletInScene.transform.position = _bulletSpawnPos.position;
                    _bulletInScene.transform.localScale = Vector3.zero;
                    _bulletInScene.SetActive(false);
                }
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
            _bulletInScene.transform.position = _bulletSpawnPos.position;
            _bulletInScene.SetActive(true);
            BulletAnimation(_bulletSize).Play();
        }
        
        private void DecreasePlayerSize()
        {
            Vector3 decreaseVector = new Vector3(_decreaseAmount, _decreaseAmount, _decreaseAmount);
            _playerInScene.transform.localScale -= decreaseVector;
            _bulletSize += decreaseVector;
        }

        private Tween BulletAnimation(Vector3 scale)
        {
            return _bulletInScene.transform.DOScale(scale, 1f).SetEase(Ease.InOutBounce);
        }
        
        private void GetSettings()
        {
            PlayerSettings playerSettings = Resources.Load<PlayerSettings>(SettingsName);
            _decreaseAmount = playerSettings.DecreaseAmount;
        }
    }
}
