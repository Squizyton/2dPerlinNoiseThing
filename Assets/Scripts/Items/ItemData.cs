using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
    public class ItemData : SerializedScriptableObject
    {
        
        [Title("Basic Variables")]
        public string itemName;
        public GameObject prefab;
        public ItemType typeOfItem;
        
        
        [Title("Inventory Variables")]
        public int maxStackAmount;
        public Sprite itemIcon;
        

        [Title("Acquirement Variables"),InfoBox("Amount of possible Item Get per pick up"),MinMaxSlider(0,100,true)]
        public Vector2 minMaxOfItemGet;




        [Title("Building Sprites"), ShowIf("@typeOfItem == ItemType.BuildingPiece")]
        public Sprite verticalSprite;
        [ShowIf("@typeOfItem == ItemType.BuildingPiece")]public Sprite horizontalSprite;
        
        
        
        
        public enum ItemType
        {
            Material,
            Weapon,
            Consumable,
            BuildingPiece
        }
    }
}
