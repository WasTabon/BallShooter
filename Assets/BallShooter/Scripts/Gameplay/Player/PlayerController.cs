using System;
using BallShooter.Scripts.Gameplay.Enviroment;
using BallShooter.Scripts.InputSystem;
using UnityEngine;
using PlayerSettings = BallShooter.Settings.Scriptables.PlayerSettings;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class PlayerController
    {
        private const string SettingsName = "PlayerSettings";
        private const float DecreaseEverySecond = 0.01f;

        public event Action OnDied; 
        public event Action<bool> OnShot; 
        
        public Bullet Bullet { get; private set; }
        
        private readonly InputManager _inputManager;
        private readonly GameObject _playerInScene;
        private readonly GameObject _bulletInScene;
        private readonly Transform _bulletSpawnPos;
        private readonly ObstacleDetector _obstacleDetector;
        private readonly PlatformController _platformController;

        private float _decreaseAmount;
        private float _bulletMoveSpeed;
        private float _timeLastDecrease;
        
        private Vector3 _bulletSize;
        private bool _isHold;
        private bool _isMovable;

        public PlayerController(InputManager inputManager, GameObject player, GameObject bullet, Transform bulletSpawnPos, ObstacleDetector obstacleDetector, PlatformController platformController)
        {
            _inputManager = inputManager;
            _playerInScene = player;
            _bulletInScene = bullet;
            _bulletSpawnPos = bulletSpawnPos;
            _obstacleDetector = obstacleDetector;
            _platformController = platformController;
        }

        public void Initialize()
        {
            _bulletSize = Vector3.zero;
            _bulletInScene.transform.localScale = _bulletSize;
            
            GetSettings();
            Bullet = new Bullet(_bulletInScene.transform, _bulletMoveSpeed);
            
            _inputManager.OnHoldPerformed += HandleInput;
            OnShot += Bullet.SetMovable;
            OnShot += SetMovable;
            _obstacleDetector.OnExplode += DisableBullet;
        }

        private void HandleInput(bool state)
        {
            if (state && !_isMovable)
            {
               PrepareToShoot();
                
               StartHolding();
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
            _bulletInScene.transform.localScale = _bulletSize;
            
            OnShot?.Invoke(true);
        }
        
        private void DecreasePlayerSize()
        {
            if (_playerInScene.transform.localScale.x >= 0f)
            {
                Vector3 decreaseVector = new Vector3(_decreaseAmount, _decreaseAmount, _decreaseAmount);
                _playerInScene.transform.localScale -= decreaseVector;
                _bulletSize += decreaseVector;   
            }
        }

        private void DisableBullet()
        {
            _bulletInScene.transform.localScale = Vector3.zero;
            _bulletSize = Vector3.zero;
            OnShot?.Invoke(false);
            
            _platformController.UpdatePlatform();
        }

        private void PrepareToShoot()
        {
            if (_isHold == false)
            {
                _bulletInScene.transform.position = _bulletSpawnPos.position;
                if (_playerInScene.transform.localScale.x <= 0.1f)
                {
                    OnDied?.Invoke();
                }
            }
        }

        private void StartHolding()
        {
            _isHold = true;
            _timeLastDecrease += Time.deltaTime;
                
            if (_timeLastDecrease >= DecreaseEverySecond)
            {
                DecreasePlayerSize();
                _timeLastDecrease = 0f;
            }
        }
        
        private void GetSettings()
        {
            PlayerSettings playerSettings = Resources.Load<PlayerSettings>(SettingsName);
            _decreaseAmount = playerSettings.DecreaseAmount;
            _bulletMoveSpeed = playerSettings.BulletMoveSpeed;
        }
        public void SetMovable(bool state)
        {
            _isMovable = state;
        }
    }
}
