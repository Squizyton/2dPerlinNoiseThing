using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Pickup;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class PlayerInventory : SerializedMonoBehaviour
    {

        private const int TOTAL_ITEM_AMOUNT = 100;

        private int currentAmountOfItemStacks;
        
        [SerializeField]private Dictionary<ItemData, ItemStack[]> items;


        private void Start()
        {

            items = new Dictionary<ItemData, ItemStack[]>();
        }

        public bool AddItem(Pickupable item)
        {
            if (currentAmountOfItemStacks >= TOTAL_ITEM_AMOUNT)
            {
                item.OnPickupFail();
                return false;
            }
            var amountGiven = Random.Range((int)item.ReturnItemData().minMaxOfItemGet.x,(int)item.ReturnItemData().minMaxOfItemGet.y);


            if (items.ContainsKey(item.ReturnItemData()))
            {
             
                var itemStack = items[item.ReturnItemData()]
                    .First(i => i.stackAmount < item.ReturnItemData().maxStackAmount);
                itemStack.stackAmount += amountGiven;

                if (itemStack.stackAmount > item.ReturnItemData().maxStackAmount)
                {
                    var difference = itemStack.stackAmount - item.ReturnItemData().maxStackAmount;
                    var newStack = new ItemStack
                    {
                        stackAmount = difference
                    };

                    items[item.ReturnItemData()].ToList().Add(newStack);
                    currentAmountOfItemStacks++;
                }
            }
            else
            {
                items.Add(item.ReturnItemData(),new []{new ItemStack(){stackAmount = amountGiven}});
            }
            
            return true;
        }


        public void AddItem()
        {
        }


        private struct ItemStack
        {
            public int stackAmount;
            public ItemData data;
        }
    }
}
