using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        
        private PlayerInput _controls;
        private Vector2 movementDir;

        [Title("References")][SerializeField] private Rigidbody2D rb;
        

        [Title("Movement Variables")] [SerializeField]
        private float movementSpeed;


        public Transform debugSphere;
        void Start()
        {
            _controls = new PlayerInput();

            _controls.Player.Movement.performed += OnMovement;
            _controls.Enable();
        }


        void OnMovement(InputAction.CallbackContext ctx)
        {
            movementDir = _controls.Player.Movement.ReadValue<Vector2>();
        }


        private void FixedUpdate()
        {
            MoveCharacter();
        }


        void MoveCharacter()
        {
            if (!movementDir.Equals(Vector2.zero))
            {
                var v = rb.velocity;

                var position = transform.position;
                var target = (Vector2) position + movementDir;


                //get the direction

                var direction = target + (Vector2) position;


                debugSphere.position = Vector2.Lerp(debugSphere.position, target, Time.deltaTime * 2f);



                var normalizedDir = movementDir.normalized;
                rb.velocity = normalizedDir * movementSpeed;


                Debug.Log(rb.velocity.sqrMagnitude);

                if (rb.velocity.sqrMagnitude > 3f)
                {
                    //rb.velocity = v.normalized * 3f;
                }
            }
            else rb.velocity = Vector3.zero;


        }
    }
}
