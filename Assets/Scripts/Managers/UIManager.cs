using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    
    [Title("Hotbar Slots")] [SerializeField]
    private HotbarSlot[] slots;



    [Title("Inventory Slots")] private InvoSlot[] _invoSlots;
    
    [SerializeField] private Transform currentSlotFrame;
    private int _currentHotbarSlot;


    private void Awake()
    {
        instance = this; 
    }



    public void UpdateInventory(PlayerInventory.ItemStack itemData)
    {
        if (ReturnAmountOfOpenHotbarSlots() > 0)
        {
            AddToHotBarSlot(itemData);
            return;
        }
        else
        {
            Debug.LogError("Joel add the f-ing inventory slots");
        }
    }

    public void AddToHotBarSlot(PlayerInventory.ItemStack itemData)
    {
        var hbSlot = slots.First(i => i.ReturnSlotItem() == null);
        hbSlot.OnIntialize(itemData);
    }

    #region Hot Bars

    public void SwitchHotbarSlot(int index)
    {
        //TODO Fix this so it uses % 
        _currentHotbarSlot += index;
        if (_currentHotbarSlot > slots.Length - 1)
        {
            _currentHotbarSlot = 0;
        }else if (_currentHotbarSlot < 0)
        {
            _currentHotbarSlot = slots.Length - 1;
        }
        currentSlotFrame.transform.position = slots[_currentHotbarSlot].transform.position;
    }

    
    
    

    public int ReturnAmountOfOpenHotbarSlots()
    {
        return slots.Where(i => i.ReturnSlotItem() == null).ToList().Count;
    }


    
    
    #endregion
}