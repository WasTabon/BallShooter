using System;
using BallShooter.Scripts.Gameplay.Enviroment;
using BallShooter.Scripts.Gameplay.Obstacles;
using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class ObstacleDetector : MonoBehaviour
    {
        [SerializeField] private float _radius;
        public event Action OnExplode; 

        public void Initialize(Transform bulletTransform)
        {
            _radius = bulletTransform.localScale.x * 1.5f;
        }
        
        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.TryGetComponent(out IObstacle _))
            {
                OnExplode?.Invoke();
                InfectObstaclesWithinRadius(coll.transform.position, _radius);
            }
            else if (coll.gameObject.TryGetComponent(out Door door))
            {
                door.FinishGame();
            }
        }

        private void InfectObstaclesWithinRadius(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out IObstacle obstacle))
                {
                    obstacle.Infect();
                }
            }
        }
    }
}
