using UnityEngine;

namespace BallShooter.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "New Settings", menuName = "Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float _decreaseAmount;
        [SerializeField] private float _bulletMoveSpeed;

        public float DecreaseAmount => _decreaseAmount;
        public float BulletMoveSpeed => _bulletMoveSpeed;
    }
}
