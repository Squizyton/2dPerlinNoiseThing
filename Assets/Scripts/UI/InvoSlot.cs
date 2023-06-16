using Inventory;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
   public abstract class InvoSlot : MonoBehaviour
   {
      [Title("Basic References")]
      [SerializeField] protected ItemData heldItem;
      [SerializeField] protected int amount;
      [SerializeField] protected Sprite itemIcon;

      public void OnIntialize(PlayerInventory.ItemStack itemStack)
      {
         heldItem = itemStack.data;
         
      }



      public ItemData ReturnSlotItem()
      {
         return heldItem;
      }

   }
}
