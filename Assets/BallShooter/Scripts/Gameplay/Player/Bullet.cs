using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class Bullet : IUpdatable
    {
        private readonly Transform _bulletTransform;

        private bool _isMovable;

        public Bullet(Transform bulletTransform)
        {
            _bulletTransform = bulletTransform;
        }
        
        public void Initialize()
        {
            
        }

        public void Update()
        {
            
        }

        public void SetMovable(bool state)
        {
            _isMovable = state;
        }
    }
}
