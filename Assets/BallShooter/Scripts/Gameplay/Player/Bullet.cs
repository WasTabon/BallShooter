using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class Bullet : IUpdatable
    {
        private readonly Transform _bulletTransform;

        private readonly float _moveSpeed;
        private bool _isMovable;

        public Bullet(Transform bulletTransform, float moveSpeed)
        {
            _bulletTransform = bulletTransform;
            _moveSpeed = moveSpeed;
        }

        public void Update()
        {
            MoveHandler();
        }

        public void SetMovable(bool state)
        {
            _isMovable = state;
        }

        private void MoveHandler()
        {
            if (_isMovable)
            { 
                Debug.Log("Moving");
                _bulletTransform.Translate(_bulletTransform.forward * (_moveSpeed * Time.deltaTime));   
            }
        }
    }
}
