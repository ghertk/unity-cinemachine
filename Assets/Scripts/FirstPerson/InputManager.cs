using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPerson
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference movementAction;
        [SerializeField]
        private InputActionReference jumpAction;
        [SerializeField]
        private InputActionReference lookAction;

        public bool PlayerJumpedThisFrame => jumpAction.action.triggered;

        void OnEnable()
        {
            movementAction.action.Enable();
            jumpAction.action.Enable();
            lookAction.action.Enable();
        }

        void OnDisable()
        {
            movementAction.action.Disable();
            jumpAction.action.Disable();
            lookAction.action.Disable();
        }

        public Vector2 GetPlayerMovement()
        {
            return movementAction.action.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return lookAction.action.ReadValue<Vector2>();
        }
    }
}