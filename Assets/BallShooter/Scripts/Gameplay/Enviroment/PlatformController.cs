using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Enviroment
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform platform;
        [SerializeField] private Transform[] obstacles;

        private Vector3 initialPlatformScale;
        private Vector3 initialPlayerScale;
        private float[] initialObstaclePositionsX;
        private float obstacleMoveFactor = 0.8f; // Фактор, контролирующий смещение препятствий

        private void Start()
        {
            initialPlatformScale = platform.localScale;
            initialPlayerScale = player.localScale;
            initialObstaclePositionsX = new float[obstacles.Length];

            for (int i = 0; i < obstacles.Length; i++)
            {
                initialObstaclePositionsX[i] = obstacles[i].localPosition.x;
            }
        }

        public void UpdatePlatform()
        {
            float scaleRatio = player.localScale.x / initialPlayerScale.x;
            
            //platform.localScale = new Vector3(initialPlatformScale.x * scaleRatio, initialPlatformScale.y, initialPlatformScale.z);

            for (int i = 0; i < obstacles.Length; i++)
            {
                Vector3 obstaclePosition = obstacles[i].localPosition;
                float targetPositionX = initialObstaclePositionsX[i] * scaleRatio * obstacleMoveFactor;
                obstacles[i].localPosition = new Vector3(targetPositionX, obstaclePosition.y, obstaclePosition.z);
            }
        }
    }
}