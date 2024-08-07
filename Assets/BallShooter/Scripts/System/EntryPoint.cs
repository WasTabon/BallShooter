using BallShooter.Scripts.Gameplay.Player;
using BallShooter.Scripts.InputSystem;
using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.System
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletSpawnPos;

        private PlayerController _playerController;
        private InputManager _inputManager;
        private Updater _updater;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeUpdater();
            InitializeInputManager();
            InitializePlayer();
        }

        private void InitializeUpdater()
        {
            GameObject created = new GameObject("Updater");
            Updater updater = created.AddComponent<Updater>();
            updater.Initialize();
            
            _updater = updater;
        }

        private void InitializeInputManager()
        {
            _inputManager = new InputManager();
            _updater.RegisterUpdatable(_inputManager);
            _inputManager.Initialize();
        }

        private void InitializePlayer()
        {
            ObstacleDetector obstacleDetector = _bullet.AddComponent<ObstacleDetector>();
            _playerController = new PlayerController(_inputManager, _player, _bullet, _bulletSpawnPos, obstacleDetector);
            _updater.RegisterUpdatable(_playerController.Bullet);
            _playerController.Initialize();
        }
    }
}
