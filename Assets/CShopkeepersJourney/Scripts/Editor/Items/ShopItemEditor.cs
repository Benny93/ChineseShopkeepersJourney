using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopItem))]
public class ShopItemEditor : Editor
{

    ShopItem _shopItem;
    public void OnEnable()
    {
        _shopItem = (ShopItem)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Inspect ShopItem"))
        {
            _shopItem.OnInspect();
        }    

    }
}


