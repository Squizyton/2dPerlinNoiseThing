using System.Collections;
using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;


/// <summary>
/// Contains all player references
/// </summary>
public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer instance;

    public PlayerInventory inventory;
    public PlayerInteraction interaction;

    void Awake()
    {
        instance = this;
    }

}
