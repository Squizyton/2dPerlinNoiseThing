using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _controls;
        private Vector2 movementDir;



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
            if (movementDir.Equals(Vector2.zero)) return;
            
            
            
            
            
        }
    }
}
