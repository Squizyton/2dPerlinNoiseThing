using System;
using Cainos.LucidEditor;
using Pickup;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public static PlayerInteraction instance;

        [SerializeField] private float interactionRadius;
        private Vector2 currentPos;
        [SerializeField] private LayerMask layerMask;
            
        
        

        public PlayerInput _controls;

        [ShowInInspector] private GameObject closestGameObject;

        private Action interactionKeyAction;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            _controls = new PlayerInput();
            _controls.Player.Interact.performed += context =>
            {
                Debug.Log("Hello?");
                interactionKeyAction?.Invoke();
            };

            _controls.Player.Hotbar.performed += HotbarSwitching;

            _controls.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            GetClosestObjectNearYou();
        }

        
#region Interaction
        public void GetClosestObjectNearYou(bool overridden = false)
        {
            if ((Vector3) currentPos != transform.position || overridden)
            {
                const int maxColliders = 10;
                var colliders = new Collider2D[maxColliders];
                var numColliders =
                    Physics2D.OverlapCircleNonAlloc(transform.position, interactionRadius, colliders, layerMask);
                var smallestDistance = float.PositiveInfinity;

                if (numColliders > 0)
                {
                    for (var i = 0; i < numColliders; i++)
                    {
                        var directionToTarget = colliders[i].transform.position - transform.position;
                        //Doing sqsrMagnitude is much more efficent then doing Vector3.Distance;
                        var dSqrToTarget = directionToTarget.sqrMagnitude;

                        if (!(dSqrToTarget < smallestDistance)) continue;
                        smallestDistance = dSqrToTarget;
                        closestGameObject = colliders[i].gameObject;
                    }

                    CheckInteractionType(closestGameObject);
                }
                else if (closestGameObject)
                {
                    closestGameObject = null;
                    interactionKeyAction += () => { Debug.Log("Is this like overriding?"); };
                }

                currentPos = transform.position;
            }
        }

        void CheckInteractionType(GameObject gO)
        {
            if (!gO.TryGetComponent<Pickupable>(out var pickupable) && gO != closestGameObject) return;
            //reset interaction new ItemStack(){stackAmount = amountGiven}key
            interactionKeyAction = null;

            Debug.Log(pickupable.name);
            interactionKeyAction += () =>
            {
                Debug.Log("Hello?");
                pickupable.OnPickup();
            };
        }

        public void RemoveClosestGameObject()
        {
            closestGameObject = null;
            interactionKeyAction = null;
        }
#endregion


#region Misc

private void HotbarSwitching(InputAction.CallbackContext ctx)
{
    UIManager.instance.SwitchHotbarSlot((int)Mathf.Clamp(ctx.ReadValue<float>() * -1f, -1, 1));

}


#endregion
        
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}