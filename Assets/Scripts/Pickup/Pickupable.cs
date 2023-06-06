using Interfaces;
using Inventory;
using Items;
using UnityEngine;

namespace Pickup
{
    public class Pickupable: MonoBehaviour,IPickupable
    {
        [SerializeField] private Rigidbody2D rb;
        
        
        [SerializeField]private ItemData itemData;


        public ItemData ReturnItemData()
        {
            return itemData;
        }


        public virtual void OnPickup()
        {
            PlayerContainer.instance.inventory.AddItem(this);
            PlayerContainer.instance.interaction.GetClosestObjectNearYou(true);
            Destroy(gameObject);
        }

        public void OnPickupFail()
        {
            rb.AddForce(Vector3.back * Random.Range(10,50),ForceMode2D.Impulse);
        }
    }
}