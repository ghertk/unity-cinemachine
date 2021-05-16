using UnityEngine;

namespace FirstPerson
{
    [RequireComponent(typeof(CharacterController), typeof(InputManager))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2f;
        [SerializeField]
        private float jumpHeight = 1f;
        [SerializeField]
        private Camera mainCamera;

        private Vector3 _playerVelocity;
        private CharacterController _characterController;
        private InputManager _inputManager;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager = GetComponent<InputManager>();
        }

        void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void Update()
        {
            if (_characterController.isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector2 movement = _inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = mainCamera.transform.forward * move.z + mainCamera.transform.right * move.x;
            _characterController.Move(move * speed * Time.deltaTime);
            move.y = 0;
            if (_inputManager.PlayerJumpedThisFrame && _characterController.isGrounded)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            }

            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }
}