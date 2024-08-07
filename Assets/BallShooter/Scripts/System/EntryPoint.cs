using BallShooter.Scripts.Gameplay.Enviroment;
using BallShooter.Scripts.Gameplay.Player;
using BallShooter.Scripts.InputSystem;
using BallShooter.Scripts.Ui;
using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.System
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletSpawnPos;
        [SerializeField] private UIView _uiView;
        [SerializeField] private Door _door;

        private PlayerController _playerController;
        private InputManager _inputManager;
        private Updater _updater;
        private UIController _uiController;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeUpdater();
            InitializeInputManager();
            InitializePlayer();
            InitializeUI();
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
            obstacleDetector.Initialize(_bullet.transform);
            
            _playerController = new PlayerController(_inputManager, _player, _bullet, _bulletSpawnPos, obstacleDetector);
            _playerController.Initialize();
            
            _updater.RegisterUpdatable(_playerController.Bullet);
        }

        private void InitializeUI()
        {
            _uiController = new UIController(_uiView);
            _playerController.OnDied += _uiController.PlayerDied;
            _door.OnFinish += _uiController.FinishGame;
        }
    }
}
