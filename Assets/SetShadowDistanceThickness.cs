using System.Collections;
using System.Collections.Generic;
using FunkyCode;
using Sirenix.OdinInspector;
using UnityEngine;

public class SetShadowDistanceThickness : MonoBehaviour
{

    public bool hello;

    [Button("Change Thickness Distance")]
    
    public void UpdateShadows()
    {
        
        
        var dayLightCollider = GetComponent<DayLightCollider2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();


        var sprite = spriteRenderer.sprite;
        dayLightCollider.shadowDistance = spriteRenderer.sprite.rect.y/8.3f;
        dayLightCollider.shadowThickness = spriteRenderer.bounds.size.x/2f;
        
        Debug.Log($"Height: {spriteRenderer.sprite.rect.y/8.3f}, Witdh:{spriteRenderer.bounds.size.x/2f}");
    }

    [Button("HEllo?")]
    public void Test()
    {
        Debug.Log("Test");
    }
}
