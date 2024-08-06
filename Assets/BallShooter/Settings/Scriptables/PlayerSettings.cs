using UnityEngine;

namespace BallShooter.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "New Settings", menuName = "Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float _decreaseAmount;

        public float DecreaseAmount => _decreaseAmount;
    }
}
