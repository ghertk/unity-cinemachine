using UnityEngine;

namespace FirstPerson
{
    [RequireComponent(typeof(Rigidbody), typeof(InputManager))]
    public class RigidbodyPlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2f;
        [SerializeField]
        private float jumpForce = 1f;
        [SerializeField]
        private Camera mainCamera;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private InputManager _inputManager;
        private Vector3 _inputs;
        private bool _isGrounded = false;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();
            _collider = GetComponent<Collider>();
            _inputs = Vector3.zero;
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
            Vector2 movement = _inputManager.GetPlayerMovement();
            _inputs = new Vector3(movement.x, 0f, movement.y);
            _inputs = mainCamera.transform.forward * _inputs.z + mainCamera.transform.right * _inputs.x;

            if (_inputManager.PlayerJumpedThisFrame && _isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
        }

        void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _inputs * speed * Time.fixedDeltaTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = false;
            }
        }
    }
}