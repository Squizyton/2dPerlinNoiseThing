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
        [SerializeField] private Animator anim;

        [Title("Movement Variables")] [SerializeField]
        private float movementSpeed;

        private bool _facingRight = true;

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
            
            if(movementDir != Vector2.zero)
                anim.SetBool("run",true);
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

                if (movementDir.x > 0f && !_facingRight)
                {
                    Flip();
                }
                if (movementDir.x < 0f && _facingRight)
                {
                    Flip();
                }
            }
            else
            {
                anim.SetBool("run",false);
                rb.velocity = Vector3.zero;
            }


        }


        private void Flip()
        {
            var currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
            
            
            _facingRight = !_facingRight;
        }
    }
}
