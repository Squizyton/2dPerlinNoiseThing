using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    
    [Title("Hotbar Slots")] [SerializeField]
    private HotbarSlot[] slots;

    [SerializeField] private Transform currentSlotFrame;
    private int _currentHotbarSlot;


    private void Awake()
    {
        instance = this; 
    }


    #region Hot Bars

    public void SwitchHotbarSlot(int index)
    {
        //TODO Fix this 
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




    #endregion
}