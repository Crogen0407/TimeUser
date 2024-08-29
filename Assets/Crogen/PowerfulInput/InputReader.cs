using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event

        public event Action<float> MoveEvent;
        public event Action JumpEvent;
        public event Action AttackEvent;

        #endregion

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                JumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }
    }
}