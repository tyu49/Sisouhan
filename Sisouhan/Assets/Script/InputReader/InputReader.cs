using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.InputReader
{
    public class InputReader : MonoBehaviour, Controller.IPlayerActions
    {
        public event Action OnNextPressed;
        public event Action OnEscPressed;
        
        private Controller _controller;

        private void Awake()
        {
            _controller = new Controller();
            _controller.Player.SetCallbacks(this);
            _controller.Player.Enable();
        }

        private void OnDestroy()
        {
            _controller.Disable();
            _controller.Dispose();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnNextPressed?.Invoke();
            }
        }

        public void OnSettingMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnEscPressed?.Invoke();
            }
        }
    }
}
