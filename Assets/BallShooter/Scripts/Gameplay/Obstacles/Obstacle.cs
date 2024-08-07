using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Obstacles
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private Material _infectMaterial;
        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Infect()
        {
            _meshRenderer.material = _infectMaterial;
            Invoke("Kill", 1f);
        }

        private void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}
