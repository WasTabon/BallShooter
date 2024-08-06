using System;
using BallShooter.Scripts.InputSystem;
using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.System
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bullet;
        
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
    }
}
