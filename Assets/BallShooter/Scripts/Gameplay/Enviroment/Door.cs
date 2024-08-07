using System;
using UnityEngine;

namespace BallShooter.Scripts.Gameplay.Enviroment
{
    public class Door : MonoBehaviour
    {
        public event Action OnFinish; 
        
        public void FinishGame()
        {
            OnFinish?.Invoke();
        }
    }
}
