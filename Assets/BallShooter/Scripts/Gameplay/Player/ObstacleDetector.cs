using BallShooter.Scripts.Gameplay.Obstacles;
using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Player
{
    public class ObstacleDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 5f;
        
        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.TryGetComponent(out IObstacle _))
            {
                InfectObstaclesWithinRadius(coll.transform.position, _detectionRadius);
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
