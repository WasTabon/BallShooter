using System;
using BallShooter.Scripts.Uitls.Updater;
using UnityEngine;

namespace BallShooter.Scripts.InputSystem
{
    public class InputManager : IUpdatable
    {
        public event Action<bool> OnHoldPerformed;
        
        private event Action OnUpdatePerformed;
        
        public void Initialize()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                OnUpdatePerformed += CheckInputWindows;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                OnUpdatePerformed += CheckInputAndroid;
            }
        }
        
        public void Update()
        {
            OnUpdatePerformed?.Invoke();
        }

        private void CheckInputAndroid()
        {
            if (Input.touchCount > 0) 
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    OnHoldPerformed?.Invoke(true);
                    return;
                }
                OnHoldPerformed?.Invoke(false);
            }
        }

        private void CheckInputWindows()
        {
            if (Input.GetMouseButton(0))
            {
                OnHoldPerformed?.Invoke(true);
                return;
            }
            OnHoldPerformed?.Invoke(false);
        }
    }
}
